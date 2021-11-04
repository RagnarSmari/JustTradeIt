import requests
import json

new_order_template = '<!DOCTYPE html><html lang="en" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:v="urn:schemas-microsoft-com:vml"><head><title></title><meta charset="utf-8"/><meta content="width=device-width, initial-scale=1.0" name="viewport"/><!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]--><style>* {box-sizing: border-box;}body {margin: 0;padding: 0;}th.column {padding: 0}a[x-apple-data-detectors] {color: inherit !important;text-decoration: inherit !important;}#MessageViewBody a {color: inherit;text-decoration: none;}p {line-height: inherit}@media (max-width:520px) {.icons-inner {text-align: center;}.icons-inner td {margin: 0 auto;}.row-content {width: 100%!important;}.stack .column {width: 100%;display: block;}}</style></head><body style="background-color: #FFFFFF; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"><table border="0" cellpadding="0" cellspacing="0" class="nl-container" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #FFFFFF;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row row-1" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row-content stack" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000;" width="500"><tbody><tr><th class="column" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;" width="100%"><table border="0" cellpadding="0" cellspacing="0" class="heading_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="width:100%;text-align:center;"><h1 style="margin: 0; color: #555555; font-size: 23px; font-family: Arial, Helvetica Neue, Helvetica, sans-serif; line-height: 120%; text-align: center; direction: ltr; font-weight: normal; letter-spacing: normal; margin-top: 0; margin-bottom: 0;"><strong>NEW TRADE REQUEST!</strong></h1></td></tr></table><table border="0" cellpadding="10" cellspacing="0" class="text_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;" width="100%"><tr><td><div style="font-family: sans-serif"><div style="font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Arial, Helvetica Neue, Helvetica, sans-serif;"><p style="margin: 0; font-size: 12px;">You just received a new trade request!! </p></div></div></td></tr></table><table border="0" cellpadding="10" cellspacing="0" class="text_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;" width="100%"><tr><td><div style="font-family: sans-serif"><div style="font-size: 12px; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2; font-family: Arial, Helvetica Neue, Helvetica, sans-serif;"><p style="margin: 0; font-size: 12px;">Remember, this very very trusted source :)</p></div></div></td></tr></table><table border="0" cellpadding="10" cellspacing="0" class="divider_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td><div align="center"><table border="0" cellpadding="0" cellspacing="0" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td class="divider_inner" style="font-size: 1px; line-height: 1px; border-top: 30px solid #BBBBBB;"><span> </span></td></tr></table></div></td></tr></table><table border="0" cellpadding="0" cellspacing="0" class="html_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td><div align="center" style="font-family:Arial, Helvetica Neue, Helvetica, sans-serif;"><div class="our-class"><img height="400" src="https://previews.123rf.com/images/konstantynov/konstantynov1106/konstantynov110600150/9763923-smiley-guy-showing-thumbs-up-isolated-on-white-background.jpg"/></div></div></td></tr></table></th></tr></tbody></table></td></tr></tbody></table><table align="center" border="0" cellpadding="0" cellspacing="0" class="row row-2" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row-content stack" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000;" width="500"><tbody><tr><th class="column" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;" width="100%"><table border="0" cellpadding="0" cellspacing="0" class="icons_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="color:#9d9d9d;font-family:inherit;font-size:15px;padding-bottom:5px;padding-top:5px;text-align:center;"><table cellpadding="0" cellspacing="0" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="text-align:center;"><!--[if vml]><table align="left" cellpadding="0" cellspacing="0" role="presentation" style="display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;"><![endif]--><!--[if !vml]><!--><table cellpadding="0" cellspacing="0" class="icons-inner" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-block; margin-right: -4px; padding-left: 0px; padding-right: 0px;"><!--<![endif]--><tr></tr></table></td></tr></table></td></tr></table></th></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><!-- End --></body></html>'


def send_newOrder_email(ch, method, properties, data):
    print("Sending new order email")
    parsed_msg = json.loads(data)
    representation = new_order_template
    send_simple_message(parsed_msg, 'You Just Received a new Trade!', representation)

def send_update_email(ch, method, properties, data):
    print("Sending update email")
    parsed_msg = json.loads(data)
    receiver = parsed_msg["Receiver"]
    representation = make_update_template(data)
    email = receiver["Email"]
    send_simple_message(email, 'There was an update to one of your trades!', representation)

def make_update_template(data):
    parsed_msg = json.loads(data)
    ## Receiver
    receiver = parsed_msg["Receiver"]
    rec_full_name = receiver["FullName"]
    rec_email = receiver["Email"]
    rec_image = receiver["ProfileImageUrl"]
    ## Receiver Items
    recItems = parsed_msg["ReceivingItems"]
    rec_items_html = ''.join(['<table> Receivers Items <br> <tr><td>%s</td><td>%s</td></tr></table>' % (item['Title'], item['ShortDescription'], ) for item in recItems ])
    
    ## Sender
    send = parsed_msg["Sender"]
    send_full_name = send["FullName"]
    send_email = send["Email"]
    send_image = send["ProfileImageUrl"]
    ## Sender Items
    offItems = parsed_msg["OfferingItems"]
    send_items_html = ''.join([ '<table> Receivers Items <br> <tr><td>%s</td><td>%s</td></tr></table>' % (item['Title'], item['ShortDescription'], ) for item in offItems ])


    date_of_issue = parsed_msg["IssuedDate"]
    date_of_mod = parsed_msg["ModifiedDate"]
    status = parsed_msg["Status"]
    return f"""<!DOCTYPE html>
<html lang="en">

<head> 
<meta charset="UTF-8"> 
<meta http-equiv="X-UA-Compatible" content="IE=edge"> 
<meta name="viewport" content="width=device-width, initial-scale=1.0"> 
<title>Document
</title>
</head>
<body>
    <div id="receiver" style="background-color: beige;">
        <h2>Receiver</h2>
        <h3>Name: {rec_full_name}</h3><br>
        <p>Email: {rec_email}</p><br>
        <p>ProfileImage</p><br>
        <img src={rec_image} width="100px" height="100px">
        <br>
        <h4>Receiver Items</h4><br>
        {rec_items_html}
    </div>

    <div id="sender" style="background-color: beige;">
        <h2>Sender</h2>
        <h3>Name: {send_full_name}</h3>
        <p>Email: {send_email}</p><br>
        <p>ProfileImage</p><br>
        <img src="{send_image}" width="100px" height="100px">
        <br>
        <h4>Sender Items</h4><br>
        {send_items_html}
    </div>

    <div id="info" style="background-color: beige;">
        <p>IssuedDate: {date_of_issue}</p><br>
        <p>ModifiedDate: {date_of_mod}</p><br>
        <p>Status: {status}</p>
    </div>


</body>
</html>"""


def send_simple_message(to, subject, body):
    print(to)
    print("Sending")
    return requests.post(
		"https://api.mailgun.net/v3/sandbox3efe989e013e4084819e9d1051eb8cf3.mailgun.org/messages",
		auth=("api", "f8605503671ef6b358df0b4d7d7dce01-2ac825a1-b7cd9c89"),
		data={"from": "Excited User <mailgun@sandbox3efe989e013e4084819e9d1051eb8cf3.mailgun.org>",
			"to": to,
			"subject": subject,
			"html": body})

