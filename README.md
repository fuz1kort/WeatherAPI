# Weather Monitoring System

## Описание

Этот проект представляет собой систему мониторинга погоды, которая состоит из трех сервисов: `Service A`, `Service B` и `Service C`. Основная цель системы — получать текущую погоду в г. Казань, публиковать её в Kafka, а затем обрабатывать и сохранять данные для последующего предоставления через REST API.

## Архитектура

1. **Service A**: 
    - Забирает данные о текущей погоде в г. Казань из открытого источника (например, OpenWeatherMap API) каждые 60 секунд.
    - Публикует данные о погоде в Kafka в топик `weather`.

2. **Service B**: 
    - Потребляет данные из топика `weather` в Kafka.
    - Отправляет данные о погоде в `Service C` через gRPC метод `SetWeather`.

3. **Service C**: 
    - Реализует метод `SetWeather` по gRPC протоколу для получения данных о погоде от `Service B`.
    - Хранит последние 10 значений погоды.
    - Предоставляет REST API для получения последних 10 значений погоды.

## Сервисы

### Service A

- **Основные функции**:
    - Получение текущих данных о погоде в г. Казань из API.
    - Публикация данных о погоде в Kafka.
  
- **Настройки**:
    - Источник данных о погоде: OpenWeatherMap API (или другой открытый API).
    - Kafka Topic: `weather`.

### Service B

- **Основные функции**:
    - Потребление сообщений из Kafka топика `weather`.
    - Отправка данных о погоде в `Service C` через gRPC.

### Service C

- **Основные функции**:
    - gRPC метод `SetWeather`: Получение данных о погоде от `Service B`.
    - Хранение последних 10 значений погоды.
    - REST API для предоставления последних 10 значений.

## Технологии

- **Kafka**: Используется для обмена сообщениями между `Service A` и `Service B`.
- **gRPC**: Используется для передачи данных от `Service B` к `Service C`.
- **REST API**: Используется `Service C` для предоставления данных пользователю.

## Установка и запуск

### 1. Клонирование репозитория

git clone https://github.com/yourusername/weather-monitoring-system.git
cd weather-monitoring-system

### 2. Настройка Kafka

1. Запустить docker-compose.yml

2. Создаем топик
    kafka-topics.sh --create --topic weather --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1

### 3. Запуск сервисов

1. https Service C
2. https Service A
3. https Servive B

### 4. Использование

GET https://localhost:7031/api/Weather/latest
