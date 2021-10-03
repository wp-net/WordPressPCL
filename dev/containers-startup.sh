#!/bin/bash
docker-compose rm -f
docker-compose build
docker-compose up --no-build -d
docker build -t script ./wordpressbot/
docker run -it --network=integration-tests-setup --name=script script
docker stop selenium && docker rm selenium
docker rm script