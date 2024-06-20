using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    public class JWTSimpleJwtLoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        [DefaultValue(null)]
        public JWTSimpleJwtLoginResponseData Data { get; set; }
    }

    public class JWTSimpleJwtLoginResponseData
    {
        [JsonProperty("user")]
        public JWTSimpleJwtLoginResponseDataUser User { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("jwt")]
        public List<JWTSimpleJwtLoginResponseDataJwt> Jwt { get; set; }
    }

    public class JWTSimpleJwtLoginResponseDataJwt
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("header")]
        public JWTSimpleJwtLoginResponseDataJwtHeader Header { get; set; }

        [JsonProperty("payload")]
        public JWTSimpleJwtLoginResponseDataJwtPayload Payload { get; set; }

        [JsonProperty("expire_in")]
        public int ExpireIn { get; set; }
    }

    public class JWTSimpleJwtLoginResponseDataJwtHeader
    {
        [JsonProperty("typ")]
        public string Typ { get; set; }

        [JsonProperty("alg")]
        public string Alg { get; set; }
    }

    public class JWTSimpleJwtLoginResponseDataJwtPayload
    {
        [JsonProperty("iat")]
        public int Iat { get; set; }

        [JsonProperty("exp")]
        public int Exp { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class JWTSimpleJwtLoginResponseDataUser
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("user_login")]
        public string UserLogin { get; set; }

        [JsonProperty("user_nicename")]
        public string UserNicename { get; set; }

        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("user_url")]
        public string UserUrl { get; set; }

        [JsonProperty("user_registered")]
        public string UserRegistered { get; set; }

        [JsonProperty("user_activation_key")]
        public string UserActivationKey { get; set; }

        [JsonProperty("user_status")]
        public string UserStatus { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
