FROM alpine:3.15
WORKDIR /app
COPY /allure-report ./allure-report
RUN apk add openjdk8
ENV PATH $PATH:/usr/lib/jvm/java-1.8-openjdk/bin
RUN apk add --update npm && npm install -g allure-commandline --save-dev
EXPOSE 9999 9999
CMD allure open "allure-report" --port 9999