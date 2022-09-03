"""
Used to calculate the distance between the students on the tag_similar_mix type dimensions.
1. use the similarities to calculate the distance between pairs.
2. use the pair distance, dimension reduced, and get the final vector for the single students.
"""
from mineCode import save
import numpy as np
from sklearn.manifold import spectral_embedding

def tagVector():
    """
    For the tag_similar and tag_mix, calculate the distance on the foundation of similarities,
    and update the vector of the students.
    :return: the students' objects, with the complete vectors.
    """
    thisColony = save.list_colony[0]

    # get the sum of the students, and prepared an array for distance
    studentSum = len(thisColony.list_students)

    # start to calculate, one dimension at a time
    for dimensionName in thisColony.dict_tagSimilarities.keys():
        # empty array, used to save
        distanceArray = np.empty((studentSum, studentSum))
        # judge the dimension
        isTagSimlar = isTagSimilar(dimensionName)

        for pairName in thisColony.dict_tagSimilarities[dimensionName]:
            pairArray = thisColony.dict_tagSimilarities[dimensionName][pairName]
            pairInfo = getPairInfo(thisColony, dimensionName, pairName, isTagSimlar)
            distanceForPair(pairName, pairArray, isTagSimlar, distanceArray, pairInfo)


        # reduced the array to make the final vector for this exact dimension
        reducedArray = spectral_embedding(distanceArray)
        vectorUpdate(thisColony, dimensionName, reducedArray)

def getPairInfo(thisColony, dimensionName, pairName, isTagSimlar):
    # the dict used to save the pair info
    pairInfo = {}

    # get the id and the student object
    idA, idB = pairName.split('_')
    student_a = thisColony.list_students[int(idA) - 1]
    student_b = thisColony.list_students[int(idB) - 1]

    if isTagSimlar:
        pairInfo.update({'student_a': student_a.dict_tagSimilar[dimensionName]})
        pairInfo.update({'student_b': student_b.dict_tagSimilar[dimensionName]})
    else:
        pairInfo.update({'student_a': student_a.dict_tagMix[dimensionName]})
        pairInfo.update({'student_b': student_b.dict_tagMix[dimensionName]})

    return pairInfo


def vectorUpdate(thisColony, dimensionName, reducedArray):
    """
    The func used to renew the vector for all the students' object, using the reduced array.
    :param thisColony: the colony with all the info.
    :param dimensionName: the name of this exact dimension, for this reduced array.
    :param reducedArray: the reduced distance array, set for the students' vectors.
    :return: change the vector in the students' object
    """
    # loop to find all the student object
    for i in range(len(thisColony.list_students)):
        # get the corresponding student and vector
        student = thisColony.list_students[i]
        vector = reducedArray[i]

        # update the student object
        student.dict_tag.update({dimensionName: vector})


def distanceForPair(pairName, pairArray, isTagSimilar, distanceArray, pairInfo):
    """
    Used to calculate the distance between the pair. For the exact dimension.
    :param pairName: the name, id, for the pair, contains two id. e.g. 1_2
    :param pairArray: the similarity array for the pair.
    :param isTagSimilar: if the type is similar(True), or mix(False).
    :param distanceArray: used to store. Used as a table, nxn, distances between all the pairs, one dimension.
    :param pairInfo: for the mix. contains the weight for the exact point(tag).
    :return: store the distance in the distanceArray.
    """
    # get the pair id
    student_a, student_b = pairName.split('_')
    stuA_arrayPosition, stuB_arrayPosition = [int(student_a) - 1, int(student_b) - 1]

    # the vector list, used to calculate the distance
    vectorList = []

    # loop to find the most similar pair
    for num in range(len(pairArray)):
        # init the three parameters to save
        maxSimilar = -1
        horizonPosition = -1
        verticalPosition = -1

        # loop to find the max similarity and the corresponding nodes
        for i in range(len(pairArray)):
            for j in range(len(pairArray)):
                thisSimilar = pairArray[i][j]
                if (thisSimilar > maxSimilar):
                    maxSimilar = thisSimilar
                    horizonPosition = i
                    verticalPosition = j

        # the gap between the gap(for mix type), or, for similar type, define the gap from save
        if isTagSimilar:
            weightGap = save.gap_noWeight
        else:
            # mix type
            print(pairInfo['student_a'])
            weight_a = list(pairInfo['student_a'].values())[horizonPosition]
            weight_b = list(pairInfo['student_b'].values())[verticalPosition]
            weightGap = abs(int(weight_a) - int(weight_b))

        # the distance between the points, for the one position
        pointGap = weightGap * 1 / maxSimilar
        vectorList.append(pointGap)

    # calculate the final distance, save in the array
    pairDistance = np.linalg.norm(vectorList)
    distanceArray[stuA_arrayPosition][stuB_arrayPosition] = pairDistance
    distanceArray[stuB_arrayPosition][stuA_arrayPosition] = pairDistance


def isTagSimilar(dimensionName):
    """
    Used to determine whether the dimension is tag_similar, or else, the mix
    :param dimensionName:
    :return:
    """
    sample = save.list_student[0]
    if dimensionName in sample.dict_tagSimilar.keys():
        return True
    else:
        return False