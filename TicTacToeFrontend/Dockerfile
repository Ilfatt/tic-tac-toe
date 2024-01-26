FROM node:14

WORKDIR /app

COPY ./TicTacToeFrontend/package*.json ./

RUN npm install

COPY ./TicTacToeFrontend/ .

EXPOSE 80

CMD ["npm", "start"]