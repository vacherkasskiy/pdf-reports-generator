FROM ubuntu:latest
LABEL authors="Vi"

# Этап 1: Сборка приложения
FROM node:latest AS build

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем файлы проекта
COPY package.json package-lock.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем остальные файлы проекта
COPY . .

# Строим приложение (можно использовать `next build` для SSR или `next export` для статической генерации)
RUN npm run build

# Этап 2: Создание итогового контейнера
FROM node:latest AS final

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем только необходимые файлы из этапа сборки
COPY --from=build /app ./

# Устанавливаем переменную среды для продакшн-режима
ENV NODE_ENV=production

# Открываем порты
EXPOSE 3000

# Запуск приложения в режиме продакшн
CMD ["npm", "start"]