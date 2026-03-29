namespace WordPressPCL.Models {
    
    /// <summary>
    /// JWT Plugins supported
    /// </summary>
    public enum JWTPlugin {
        
        /// <summary>
        /// JWT Authentication for WP REST API plugin
        /// Author - Enrique Chavez
        /// </summary>
        JWTAuthByEnriqueChavez,
        /// <summary>
        /// JWT Auth – WordPress JSON Web Token Authentication plugin
        /// Author - Useful Team
        /// </summary>
        JWTAuthByUsefulTeam,
        /// <summary>
        /// simple-jwt-login – WordPress JSON Web Token Authentication plugin
        /// https://wordpress.org/plugins/simple-jwt-login/
        /// Author - Nicu Micle
        /// </summary>
        JWTSimpleJwtLogin

    }
}
