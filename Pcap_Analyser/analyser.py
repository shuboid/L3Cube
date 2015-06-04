from Tkinter import *
import tkMessageBox
from tkFileDialog import askopenfilename
import os

master=Tk()
master.title("Pcap File Analyser")
filename=""
e1 =Entry(master)
e2 =Text(master,width=150)
e1.grid(row=1,column=0)
e2.grid(row =5 )
e1.insert(50, "No file choosen yet")


def choosefile():
    #master.withdraw() # we don't want a full GUI, so keep the root window from appearing
    filename = askopenfilename()
    e1.delete(0,END)
    e1.insert(50, filename) # show an "Open" dialog box and return the path to the selected file
    
def fileanalyse():
    initial= " tcpdump -ttttnnr " + e1.get()+" | cat > output.txt"
    print(e1.get())
    os.system(initial)
    fo =open("output.txt")
    string1 =fo.read()
    e2.insert(INSERT,string1)
    e1.delete(0,END)
    


Label(master, text="Pcap file path :").grid(row=0)
Label(master, text="data in file").grid(row=4)
Button(master, text='Choose file', command=choosefile).grid(row=2, sticky=W, pady=4)
Button(master, text='Analyse', command=fileanalyse).grid(row=3, sticky=W, pady=4)
e1.insert(10, filename)
mainloop()

