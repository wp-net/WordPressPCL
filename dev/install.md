Run `docker compose up -d` inside the `dev` folder and it will start a fully setup Docker WordPress instance on port 8080 to run tests.

Wait until the setup container reports `WordPress test environment is ready.` in `docker compose logs cli`, or until `/var/www/html/.wp-tests-ready` exists inside the `wordpress` container.

- The WordPress instance will have following already configured plugins:
  - [JWT Auth – WordPress JSON Web Token Authentication](https://wordpress.org/plugins/jwt-auth/)
  - [Contact Form 7](https://wordpress.org/plugins/contact-form-7/)
  - [Application Passwords Enable](https://wordpress.org/plugins/application-passwords-enable/)

- The "Permalinks" link structure in WordPress settings is set to "Post name"

- The environment uses pinned official `mariadb` and `wordpress` images, and the custom WordPress image includes the official `wp-cli` binary so local development and CI exercise the same stack more reliably.

To destroy the containers simply run `docker compose down` in the terminal

To run the tests in Visual Studio, make sure to use the `jwtauth.runsettings` test settings.
