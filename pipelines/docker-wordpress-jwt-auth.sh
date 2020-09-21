#delete master repo
rm -rf wordpress-docker-compose

# Download a wordpress docker-compose example
git clone -b plugins/jwt-auth https://github.com/wp-net/wordpress-docker-compose
cd wordpress-docker-compose
# Automatic installation of wordpress
make autoinstall

docker ps

curl -Is http://localhost | head -1