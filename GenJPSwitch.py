from os import listdir
from os import path
from os.path import isfile, join, splitext, abspath, realpath, basename, relpath
import json
import os
import re
import sys

import importlib.util

def main():
    filename = path.join(os.path.dirname(__file__), "ItemListJP.txt")
    print(filename)
    
    lines = []
    itemId = 1
    for filelineno, line in enumerate(open(filename, encoding="utf-8")):
        line = "case " + hex(itemId) + ": return \"" + line.replace("\n", "") + "\";"
        lines.append(line)
        itemId = itemId + 1
    
    for line in lines:
        print(line)

if __name__ == '__main__':
    main()
  