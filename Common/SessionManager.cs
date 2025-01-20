
namespace ARTHVATECH_ADMIN.Common
{
    public class SessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.Session.GetString("UserID");

                if (!string.IsNullOrEmpty(userId))
                {
                    return Guid.Parse(userId);
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value.HasValue)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("UserID", value.ToString());
                }
            }
        }
    }
}
