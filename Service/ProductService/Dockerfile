FROM node:12

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

COPY . .

# docker-compose file a alınacak ! Burda kalmamalı !
ENV ENVIRONMENT=DEV

EXPOSE 3000
CMD [ "node", "server.js" ]