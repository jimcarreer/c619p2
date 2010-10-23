import csv

input = open('./people.csv', 'rb')
output = open('./people.dat', 'w')
preader = csv.reader(input, delimiter=',', quotechar='|')
for row in preader:
  output.write(row[0] + "," + row[1] + "," + row[3] + "\n")
input.close()
output.close()