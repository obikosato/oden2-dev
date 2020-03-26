##################################################
## File: LineMessenger.py
## Author: Obiko Sato
## Copyright: Copyright 2020, Oden
## Email: Obiko.Sato@jp.ricoh.com
##################################################

import sys
import requests
import datetime

def lineNotify(token, message):
    line_notify_token = token
    line_notify_api = 'https://notify-api.line.me/api/notify'
    payload = {'message': message}
    headers = {'Authorization': 'Bearer ' + line_notify_token} 
    r = requests.post(line_notify_api, data=payload, headers=headers)
    return r.status_code

try:
    if len(sys.argv) == 3:
        print(lineNotify(sys.argv[1], sys.argv[2].replace("　"," ")))
    else:
        print('ArgumentException...')
except:
    print("Unexpected error:", sys.exc_info()[0])