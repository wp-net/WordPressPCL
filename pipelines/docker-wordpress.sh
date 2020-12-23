# Download a wordpress docker-compose example
git clone https://github.com/wp-net/wordpress-docker-compose
cd wordpress-docker-compose
# Automatic installation of wordpress
make autoinstall

docker ps

curl -Is http://localhost | head -1