using System.Collections.Concurrent;

namespace inventorybackend.Api.Middleware
{
    public class RateLimitingMiddleware
    {
        private static readonly ConcurrentDictionary<string, TokenBucket> _buckets = new();
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly int _maxRequests;
        private readonly int _refillRate;
        private readonly int _refillPeriod;

        public RateLimitingMiddleware(
            RequestDelegate next,
            ILogger<RateLimitingMiddleware> logger,
            int maxRequests = 100,
            int refillRate = 10,
            int refillPeriod = 60)
        {
            _next = next;
            _logger = logger;
            _maxRequests = maxRequests;
            _refillRate = refillRate;
            _refillPeriod = refillPeriod;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = GetClientKey(context);
            var bucket = _buckets.GetOrAdd(key, _ => new TokenBucket(_maxRequests, _refillRate, _refillPeriod));

            if (!bucket.TryConsume())
            {
                _logger.LogWarning("Rate limit exceeded for client {ClientKey}", key);
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsJsonAsync(new { error = "Rate limit exceeded. Please try again later." });
                return;
            }

            await _next(context);
        }

        private string GetClientKey(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }

    public class TokenBucket
    {
        private readonly int _maxTokens;
        private readonly int _refillRate;
        private readonly int _refillPeriod;
        private double _tokens;
        private DateTime _lastRefill;

        public TokenBucket(int maxTokens, int refillRate, int refillPeriod)
        {
            _maxTokens = maxTokens;
            _refillRate = refillRate;
            _refillPeriod = refillPeriod;
            _tokens = maxTokens;
            _lastRefill = DateTime.UtcNow;
        }

        public bool TryConsume()
        {
            RefillTokens();
            if (_tokens >= 1)
            {
                _tokens -= 1;
                return true;
            }
            return false;
        }

        private void RefillTokens()
        {
            var now = DateTime.UtcNow;
            var timePassed = (now - _lastRefill).TotalSeconds;
            var tokensToAdd = timePassed * (_refillRate / (double)_refillPeriod);
            _tokens = Math.Min(_maxTokens, _tokens + tokensToAdd);
            _lastRefill = now;
        }
    }
} 