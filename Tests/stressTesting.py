import requests
import sys
import threading
import time

usecase = sys.argv[1]
getAllUrl = 'http://localhost:57740/api/records'
translateUrl = "http://localhost:57740/api/processing/translate?text=Ana are mere"
threads = []
successes = 0
start_time = time.time()

def makeRequest(url):
    global successes
    response = requests.get(getAllUrl)
    if(response.status_code == 200):
        successes += 1

if(usecase == '1'):
    for i in range(10):
        thread = threading.Thread(target=makeRequest, args=[getAllUrl])
        threads.append(thread)
        thread.start()
if(usecase == '2'):
    for i in range(100):
        thread = threading.Thread(target=makeRequest, args=[getAllUrl])
        threads.append(thread)
        thread.start()
if(usecase == '3'):
    for i in range(1000):
        thread = threading.Thread(target=makeRequest, args=[getAllUrl])
        threads.append(thread)
        thread.start()
if(usecase == '4'):
    for i in range(10):
        thread = threading.Thread(target=makeRequest, args=[translateUrl])
        threads.append(thread)
        thread.start()
if(usecase == '5'):
    for i in range(100):
        thread = threading.Thread(target=makeRequest, args=[translateUrl])
        threads.append(thread)
        thread.start()
if(usecase == '6'):
    for i in range(1000):
        thread = threading.Thread(target=makeRequest, args=[translateUrl])
        threads.append(thread)
        thread.start()


for thread in threads:
    thread.join() 

print("Number of requests: " + str(len(threads)) + "  \nNumber of successes: " + str(successes) + " \nElapsed time: " + str(time.time() - start_time))