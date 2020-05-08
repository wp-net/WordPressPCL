# Download a wordpress docker-compose example
git clone https://github.com/wp-net/wordpress-docker-compose
cd wordpress-docker-compose
# Automatic installation of wordpress
make autoinstall

curl -Is http://localhost | head -1

docker exec wordpress_wpcli wp theme install quark
