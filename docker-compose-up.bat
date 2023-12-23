docker-compose --project-name svsttest02 down
rem docker system prune -a
docker rmi svsttest02
docker rmi svsttest002client
docker-compose --project-name svsttest02 up --force-recreate -d