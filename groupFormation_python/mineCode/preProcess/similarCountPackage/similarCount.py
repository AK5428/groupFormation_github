"""
Used to calculate and store the similarities for tag_mix and tag_similar.
Get the students' object info from save.
"""
from mineCode.preProcess.colonyClass import Colony
from mineCode import save
from mineCode.preProcess.similarCountPackage.countAndThreshold import similarCount_withPair
import numpy as np

def similarCountTrigger():
    # the path of the npy
    folderName = save.ORIGINAL_FOLDER.split('_')[-1]
    savePath = save.SIMILARITY_FOLDER + '/' + folderName + '.npy'

    if not save.isFromFile:
        similarCount(savePath)
    else:
        savePath = save.SIMILARITY_FOLDER + '/' + folderName + '.npy'
        save.list_colony = np.load(savePath, allow_pickle=True)

        # an error occurs, for the former class of student, I didn't define the dict_tag.
        # so I have to update the students' objects.
        save.list_colony[0].list_students = save.list_student


    return

def similarCount(savePath):
    # put the students' list in the colony class.
    folderName = save.ORIGINAL_FOLDER.split('_')[1]
    thisColony = Colony(save.list_student, folderName)
    save.list_colony.append(thisColony)

    # get the tags from tag_similar
    if thisColony.hasTagSimilar:
        pairDictCreate_tagSimilar(thisColony)

    # get the tags from tag_mix:
    if thisColony.hasTagMix:
        pairDictCreate_tagMix(thisColony)

    # calculate the similarities for the pair in pairDict
    similarCount_withPair(thisColony)

    # save the similarities in npy, from the colony object in save, in the list
    np.save(savePath, save.list_colony, allow_pickle=True)


def pairDictCreate_tagSimilar(thisColony):
    """
    To get the pair for all the tagSimilar type, and save in the dict.
    :param thisColony: the colony of this class.
    :return: the saved pair in colony
    """
    # get the dimension names for the tagSimilar
    dimensionNames = list(thisColony.list_students[0].dict_tagSimilar.keys())

    # for all dimensions that is tagSimilar, create the pair dict
    # for each dimension
    for name in dimensionNames:
        dimensionDict = {}

        # for all student pairs
        for i in range(len(thisColony.list_students)):
            for j in range(i + 1, len(thisColony.list_students)):
                # find the two students, make them the pair
                student_a = thisColony.list_students[i]
                student_b = thisColony.list_students[j]
                # get the tags in the list
                list_a = student_a.dict_tagSimilar[name]
                list_b = student_b.dict_tagSimilar[name]
                # combined into pair name
                pairName = student_a.stuNum + '_' + student_b.stuNum

                # store the pair into dict
                dimensionDict.update({pairName: {'student_a': list_a, 'student_b': list_b}})

        # update the colony's dict
        thisColony.dict_tagPair.update({name: dimensionDict})


def pairDictCreate_tagMix(thisColony):
    """
    To get the pair for all the tagMix type, and save in the dict.
    :param thisColony: the colony of this class.
    :return: the saved pair in colony
    """
    # get the dimension names for the tagMix
    dimensionNames = list(thisColony.list_students[0].dict_tagMix.keys())

    # for all dimensions that is tagSimilar, create the pair dict
    # for each dimension
    for name in dimensionNames:
        dimensionDict = {}

        # for all student pairs
        for i in range(len(thisColony.list_students)):
            for j in range(i + 1, len(thisColony.list_students)):
                # find the two students, make them the pair
                student_a = thisColony.list_students[i]
                student_b = thisColony.list_students[j]
                # get the tags in the list
                list_a = list(student_a.dict_tagMix[name].keys())
                list_b = list(student_b.dict_tagMix[name].keys())
                # combined into pair name
                pairName = student_a.stuNum + '_' + student_b.stuNum

                # store the pair into dict
                dimensionDict.update({pairName: {'student_a': list_a, 'student_b': list_b}})

        # update the colony's dict
        thisColony.dict_tagPair.update({name: dimensionDict})

