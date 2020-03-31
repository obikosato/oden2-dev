import sys
import requests
import datetime
from bs4 import BeautifulSoup

URL = 'https://www.yokohama-arena.co.jp/'
headers = {"User-Agent": "hoge"}

resp = requests.get(URL, timeout=1, headers=headers)
r_text = resp.text

soup = BeautifulSoup(r_text, 'html.parser')

event_day = soup.find("span", class_="date-display-single")
artist_name = soup.find("p", class_="artist")
event_name = soup.find("p", class_="title")

if event_day == None:
    date = ''
    artist = ''
    title = '' 

else:
    date = event_day.get_text()
    artist = '' if artist_name == None else artist_name.get_text()
    title = '' if event_name == None else event_name.get_text()
    
d = {'date': date, 'artist': artist, 'title' : title}
print(d)