"""
Used to process the input of the tag type.
1. mix = weight + similar
2. weight: no need to calculate the similarities
3. similar
"""

import csv
import mineCode.save as save

def tagWeightProcess(filePath, dimensionName):
    """
    Used to input the weight tag.
    :param filePath: the path of the csv.
    :param dimensionName: the dimensionName from the fileName.
    :return: none
    """
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    tagStartPosition = 2
    contentList.pop(0)

    for i in range(len(contentList)):
        weightList = contentList[i][tagStartPosition:]
        save.list_student[i].dict_tagWeight.update({dimensionName: weightList})

def tagSimilarProcess(filePath, dimensionName):
    """
    Used to input the similar tag.
    :param filePath: the path of the csv.
    :param dimensionName: the dimensionName from the fileName.
    :return: none
    """
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    tagStartPosition = 2
    contentList.pop(0)

    for i in range(len(contentList)):
        tagList = contentList[i][tagStartPosition:]
        save.list_student[i].dict_tagSimilar.update({dimensionName: tagList})

def tagMixProcess(filePath, dimensionName):
    """
    Used to input the similar tag.
    Need to correspond the tag and the weight.
    :param filePath: the path of the csv.
    :param dimensionName: the dimensionName from the fileName.
    :return: none
    """
    # open the csv file
    with open(filePath, 'r', encoding='UTF-8-sig') as csvFile:
        content = csv.reader(csvFile)
        contentList = list(content)

    # get the headerRow
    headerRow = contentList.pop(0)

    # process the headerRow to separate the tag and the weight
    # find the first position of the tag, and the first position of weight
    tagStartPosition = 2
    weightStartPosition = 0
    tagHeaderName = headerRow[tagStartPosition].split('_')[0]
    for header in headerRow:
        if(header.split('_')[0] == tagHeaderName):
            weightStartPosition = headerRow.index(header)

    # the end and start position
    weightStartPosition += 1
    positionGap = weightStartPosition - tagStartPosition

    # read the csv in row
    for i in range(len(contentList)):
        row = contentList[i]
        # the dict used to store this dimension info
        dimensionDict = {}
        for index in range(tagStartPosition, tagStartPosition + positionGap):
            dimensionDict.update({row[index]: row[index + positionGap]})

        save.list_student[i].dict_tagMix.update({dimensionName: dimensionDict})