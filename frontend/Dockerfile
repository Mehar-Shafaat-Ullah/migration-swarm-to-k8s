# Stage 1: Build React
FROM node:18-alpine AS build-step
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . ./
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine
# Copy our custom config from the nginx folder
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf
# Copy the React build files
COPY --from=build-step /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
