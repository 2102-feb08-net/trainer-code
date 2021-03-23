# docker big picture:
# write a Dockerfile (instructions to create an image)
# build an image from a Dockerfile (docker build)
# run containers from that image   (docker run)

# steps for using this app (from this directory):
# 1. dotnet publish -o publish
# 2. docker build -t consoleapp:1.0 .
# 3. docker run consoleapp:1.0

# normally, you make your images by adding new layers onto existing images
# the first thing to think about is what base image will you choose.

# 1. official/well-supported/maintained base images
# 2. as many dependencies of your app/code already set up
#    (so you don't have to do it yourself with your own layers)

# first command in a Dockerfile: FROM, to identify the base image.

#      registry
#   (docker.io aka
#    docker hub         repository
#   by default)           name        tag
#    -----------------|--------------|----
FROM mcr.microsoft.com/dotnet/runtime:5.0

# copy from the build context to the image's file system
COPY publish/ /app

# the syntax for CMD is a little odd, each space-separated thing that the shell
# should see should be a separate item in this list
CMD [ "dotnet", "/app/ContainerizedConsoleApp.dll" ]
# CMD doesn't run anything at build-time, it sets metadata:
#  the default command used with dotnet run.

# docker build
#    1. needs a build context (a directory)
#        the whole directory will be uploaded by the docker client
#        to the docker daemon.
#    2. needs a dockerfile. (by default, looks in <build-context>/Dockerfile)
#         (uploaded to the daemon as well)
#    3. docker goes line by line through the Dockerfile -
#       for FROM, will pull the image if needed
#       will run any other commands in the Dockerfile to build the image layer by layer
