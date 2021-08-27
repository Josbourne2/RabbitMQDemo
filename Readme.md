# Rabbit MQ Demo
## _Just a little demo_

### Setting up
Before you continue, you would like to install docker to run the Rabbit MQ message broker in a container on your desktop. It's the easiest way to get up and running with this demo.

First, install docker from https://docs.docker.com/desktop/windows/
Next, run the following in command prompt: ```docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management``` 
Next, run the following statement to start a rabbitmq container: ```docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management```

After you have performed these steps you can login to the RabbitMQ management interface on localhost:15672 and login with user guest and password guest.
This will allow you to see statistics, just go through the interface and find out where you can see the queues that exist and what their parameters are. Play around with creating new queues and google the parameter settings.

