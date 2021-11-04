FROM python:latest
LABEL maintainer="Ragnar Smári"

COPY . /app
WORKDIR /app

RUN pip install -r requirements.txt

CMD [ "python3", "service.py" ]