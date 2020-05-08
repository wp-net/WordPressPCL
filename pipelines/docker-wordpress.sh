# Download a wordpress docker-compose example
git clone https://github.com/kassambara/wordpress-docker-compose
cd wordpress-docker-compose
# Automatic installation of wordpress
make autoinstall

docker exec wordpress_wpcli wp theme install quark
