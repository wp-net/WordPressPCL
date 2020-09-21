#delete master repo
mkdir wordpress-docker-compose-jwt-auth
cd wordpress-docker-compose-jwt-auth

# Download a wordpress docker-compose example
git clone -b plugins/jwt-auth https://github.com/wp-net/wordpress-docker-compose
cd wordpress-docker-compose
# Automatic installation of wordpress
make autoinstall

docker ps

curl -Is http://localhost | head -1