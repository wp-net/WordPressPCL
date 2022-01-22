Run `containers-startup` script inside the `dev` folder (`sh` for Mac and Linux and `ps1` for Windows), which will start a fully setup dockerzied Wordpress instance on localhost port 80 to run tests against.

- The Wordpress instance will have following already configured plugins:
  - JWT Auth – WordPress JSON Web Token Authentication
  - Contact Form 7
  - https://github.com/wp-net/wordpress-docker-compose/raw/master/plugins/enable-application-passwords.1.0.zip

- The "Permanlinks" link structure in Wordpress settings is set to "Post name"

To destroy the containers simply run `docker-compose down` in the terminal