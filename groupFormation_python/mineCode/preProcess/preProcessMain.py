"""
The main py as the starters for the preProcess.
1. build the structure of the student class.
2. read all info of the dimensions from the target folder.
3. divide the process into several steps, according to the type of the dimension.
    1) the tag type (similar and mix),
        where the similarities need to be calculated, need to be processed first.
    2) the network and the tag type (with similar), + dimension reduced.
    3) the other type can be stored into class directly.
"""

import mineCode.save as save
import os
import tagInputProcess as preTag
import othersInputProcess as preOthers
import csv
from mineCode.preProcess.studentClass import Student
from similarCountPackage.similarCount import similarCountTrigger
import time
from tagDistance import tagVector
import numpy as np

def preProcessStart():
    # read all the info into class
    dataInput()

    # similar count, used to calculate all the similarities for mix/similar kind of tag type.
    similarCountTrigger()

    # after the similarity is count, need to calculate the distance between the students, on these dimensions
    tagVector()
    # combine the tags
    for student in save.list_colony[0].list_students:
        student.dict_tag = student.dict_tag | student.dict_tagWeight

    # store the final result into npy
    npyName = save.ORIGINAL_FOLDER.split('_')[-1] + '.npy'
    npyPath = save.PRE_PROCESSED_FOLDER + '/' + npyName
    np.save(npyPath, save.list_colony, allow_pickle=True)

    for student in save.list_colony[0].list_students:
        print(student)


    return




def dataInput():
    """
    Read all the csv files, divide it with the category, and store in the class.
    :return: the list_student in save, with all the original info in it.
    """
    for dirpath, dirnames, filenames in os.walk(save.ORIGINAL_FOLDER):
        for filename in filenames:
            # the absolute path of the file
            filePath = os.path.join(dirpath, filename)
            # the name of the file
            fileName = os.path.join(filename)


            # get the name and type for each dimension
            dimensionName = fileName.split('.')[0].split('_')[0]
            dimensionType = fileName.split('.')[0][len(dimensionName) + 1:]

            # build the students object
            if dimensionName == 'classNameList':
                studentListBuild(filePath)

            # store the dimension info
            if dimensionType == 'value':
                preOthers.valueProcess(filePath, dimensionName)
            elif dimensionType == 'type':
                preOthers.typeProcess(filePath, dimensionName)
            elif dimensionType == 'network':
                preOthers.networkProcess(filePath, dimensionName)

            # fill in the other info, for the tags
            if dimensionType.split('_')[0] == 'tag':
                if dimensionType.split('_')[1] == 'similar':
                    preTag.tagSimilarProcess(filePath, dimensionName)
                elif dimensionType.split('_')[1] == 'mix':
                    preTag.tagMixProcess(filePath, dimensionName)
                elif dimensionType.split('_')[1] == 'weight':
                    preTag.tagWeightProcess(filePath, dimensionName)

    # print the student objects to check
    printStuObject()

def printStuObject():
    """
    Used to print all the students' objects to check.
    :return: none
    """
    for stuObject in save.list_student:
        print(stuObject)
    print(Student.typeDict)

def studentListBuild(filePath):
    """
    Used to build all the students' objects with the name list, and store it in the save, prepared for later.
    :param filePath: the path of the name list.
    :return: the list in save.
    """
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    for row in contentList[1:]:
        stuObject = Student(row[0], row[1], row[2])
        save.list_student.append(stuObject)

def printTime(seconds):
    """
    Used to print the run time
    :param seconds:
    :return:
    """
    m, s = divmod(seconds, 60)
    h, m = divmod(m, 60)
    print("%02d:%02d:%02d" % (h, m, s))


if __name__ == '__main__':
    # mark the time
    start = time.time()

    # start the process
    preProcessStart()

    # print the used time
    end = time.time()
    runTime = end - start
    printTime(runTime)
