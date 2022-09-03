"""
Used to input and store the info other than tag.
1. value
2. type
3. network
"""
import mineCode.save as save
import csv
from mineCode.preProcess.studentClass import Student
import copy
import numpy as np
from sklearn.manifold import spectral_embedding

def valueProcess(filePath, dimensionName):
    """
    Used to process the input of value type.
    :param filePath: the csv file.
    :param dimensionName:
    :return:
    """
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    tagStartPosition = 2
    # pop the header row
    contentList.pop(0)

    for i in range(len(contentList)):
        value = contentList[i][tagStartPosition]
        save.list_student[i].dict_value.update({dimensionName: value})

def typeProcess(filePath, dimensionName):
    """
    Used to process the input of type.
    And store all the type into class, as public.
    :param filePath:
    :param dimensionName:
    :return:
    """
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    tagStartPosition = 2
    # pop the header row
    contentList.pop(0)

    # used to save all the types, and sum it
    typeList = []

    for i in range(len(contentList)):
        type = contentList[i][tagStartPosition]
        typeList.append(type)
        save.list_student[i].dict_type.update({dimensionName: type})

    # save the type info in the class
    typeList = list(set(typeList))
    Student.typeDict.update({dimensionName: typeList})

def networkProcess(filePath, dimensionName):
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    # pop the header row
    contentList.pop(0)

    # prepare the pure data list for dimension reducing
    pureDataList = copy.deepcopy(contentList)
    for row in pureDataList:
        row.pop(0)
        row.pop(0)
        for i in range(len(row)):
            row[i] = float(row[i])

    # turn it into data array, and make it symmetric
    dataArray = np.array(pureDataList)
    for i in range(len(dataArray)):
        for j in range(len(dataArray)):
            if dataArray[i][j] == 1.0:
                dataArray[i][j] = save.distanceClose
                dataArray[j][i] = save.distanceClose
            else:
                dataArray[i][j] = save.distanceFar
                dataArray[j][i] = save.distanceFar

    # reduce it
    reducedArray = spectral_embedding(dataArray)

    for i in range(len(reducedArray)):
        networkList = reducedArray[i]
        save.list_student[i].dict_network.update({dimensionName: networkList})


    return
