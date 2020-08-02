docker build -t grpcservice101 .
docker run --rm -it --name grpcservice101 -p 5000:80 grpcservice101