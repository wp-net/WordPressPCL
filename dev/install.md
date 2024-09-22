Run `docker compose up` inside the `dev` folder and it will start a fully setup docker Wordpress instance on port 8080 to run tests.

- The Wordpress instance will have following already configured plugins:
  - [JWT Auth – WordPress JSON Web Token Authentication](https://wordpress.org/plugins/jwt-auth/)
  - [Contact Form 7](https://wordpress.org/plugins/contact-form-7/)
  - https://github.com/wp-net/wordpress-docker-compose/raw/master/plugins/enable-application-passwords.1.0.zip

- The "Permanlinks" link structure in Wordpress settings is set to "Post name"

To destroy the containers simply run `docker compose down` in the terminal

To run the tests in Visual Studio, make sure to use the `jwtauth.runsettings` test settings.