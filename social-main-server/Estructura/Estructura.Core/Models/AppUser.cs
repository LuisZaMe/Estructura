using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Estructura.Core.Models
{

    public class AppUser
    {
        public static string CLAIM_USER = "sub";
        public static string CLAIM_USER_ID = "userID";
        public static string CLAIM_EMAIL = "Email";
        public static string CLAIM_USER_ROLE = "UserRole";

        //Added, ONLY REFRESH TOKENS
        public static string CLAIM_CHALLENGE = "Challenge";
        public static string CLAIM_ISSUED_SERVER_DATE = "IssuedServerDate";
        public static string CLAIM_DAYS_TO_LIVE = "DaysToLive";


        private readonly ClaimsPrincipal _claimsPrincipal;
        private string _user;
        private int? _userId;
        private string _email;
        private Role _userRole;

        public AppUser(IPrincipal principal)
        {
            _claimsPrincipal = principal as ClaimsPrincipal;
            if (_claimsPrincipal!=null&&_claimsPrincipal.Identity!=null)
                IsAuthenticated = _claimsPrincipal.Identity.IsAuthenticated;
            else
                IsAuthenticated = false;
            if (_claimsPrincipal == null)
                throw new Exception("Expected ClaimsPrincipal");
        }

        public bool IsAuthenticated { get; }

        public string User
        {
            get
            {
                if (!string.IsNullOrEmpty(_user)) return _user;

                var claim = _claimsPrincipal.FindFirst(CLAIM_USER);

                if (claim == null) throw new Exception("Could not locate UserID/sub claim");

                _user = claim.Value;

                return _user;
            }
        }

        public int UserID
        {
            get
            {
                if (_userId.HasValue) return _userId.Value;

                var claim = _claimsPrincipal.FindFirst(CLAIM_USER_ID);

                if (claim == null) throw new Exception("Could not locate UserID claim");

                _userId = int.Parse(claim.Value);

                return _userId.Value;
            }
        }


        public string Email
        {
            get
            {
                if (!string.IsNullOrEmpty(_email)) return _email;

                var claim = _claimsPrincipal.FindFirst(CLAIM_EMAIL);

                if (claim == null) throw new Exception("Could not locate Email claim");

                _email = claim.Value;

                return _email;
            }
        }

        public Role UserRole
        {
            get
            {
                if (_userRole != Role.NONE) return _userRole;

                var claim = _claimsPrincipal.FindFirst(CLAIM_USER_ROLE);

                if (claim == null) throw new Exception("Could not loacate UserrOLE claim");

                _userRole = (Role)int.Parse(claim.Value);

                return _userRole;
            }
        }
    }
}
