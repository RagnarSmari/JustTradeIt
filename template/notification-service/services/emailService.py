import pika
import requests
import json

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

# Declare the exchange, if it doesnt exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
#Declare the queue, if it doesnt exist
channel.queue_declare(queue=email_queue_name, durable=True)
#Bind the queue to a specific exchange with a routing key
channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)


def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/https://"
        "app.mailgun.com/app/sending/domains/sandbox3a56a9413b1c422e9679e862e64abd97.mailgun.org/messages",
        auth=("api", "344033346b6a9cf87bbc7769de4d7885-2ac825a1-421a65d0"),
        data={"from": "Mailgun Sandbox <postmaster@shttps://"
                      "app.mailgun.com/app/sending/domains/sandbox3a56a9413b1c422e9679e862e64abd97.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})

def send_order_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    email = parsed_msg['email']
    items = parsed_msg['items']
    items_html = ''.join(['<tr><td>%s</td><td>%d</td><td>%d</td><td>%d</td></tr>' % (
    item['description'], item['unitPrice'], item['quantity'], int(item['quantity']) * int(item['unitPrice'])) for item
                          in items])
    representation = email_template % items_html
    send_simple_message(parsed_msg['email'], 'Successful order!', representation)


    channel.basic_consume(send_order_email,
                          queue=email_queue_name,
                          no_ack=True)

channel.start_consuming()
connection.close()