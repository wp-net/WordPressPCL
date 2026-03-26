Run `docker compose up -d` inside `/home/runner/work/WordPressPCL/WordPressPCL/dev` and it will start a fully setup Docker WordPress instance on port 8080 to run tests.

Wait until the setup container reports `WordPress test environment is ready.` in `docker compose logs cli`, or until `/var/www/html/.wp-tests-ready` exists inside the `wordpress` container.

- The Wordpress instance will have following already configured plugins:
  - [JWT Auth – WordPress JSON Web Token Authentication](https://wordpress.org/plugins/jwt-auth/)
  - [Contact Form 7](https://wordpress.org/plugins/contact-form-7/)
  - https://github.com/wp-net/wordpress-docker-compose/raw/master/plugins/enable-application-passwords.1.0.zip

- The "Permanlinks" link structure in Wordpress settings is set to "Post name"

- The environment uses pinned official `mariadb`, `wordpress`, and `wordpress:cli` images so local development and CI exercise the same stack more reliably.

To destroy the containers simply run `docker compose down` in the terminal

To run the tests in Visual Studio, make sure to use the `jwtauth.runsettings` test settings.
