"""
This package is used to combine the homo and heter dimensions, and calculate the mahaDistance,
store the distance between each two students, saved in array, prepared for later.
1. combine the homo/heter dimension, saved in the students' objects.
2. calculate the mahaDistance, then saved into the save py.
"""
from mineCode.genetic_groupSelect.mahaDistance.dataArrage import dataArrangeStart
from mineCode.genetic_groupSelect.mahaDistance.mahaCalculate import mahaDistanceStart
from mineCode import save
import numpy as np
import copy

def mahaDistanceTrigger():
    # data arrange, we need to combine the homo and heter separately.
    dataArrangeStart()
    # maha distance calcualted
    mahaDistanceStart()

    # get the max and min num, as the values can be calculated from this
    borderValue()

def borderValue():
    """
    Sort the maha distance array, get the min and max distance, save it.
    This border value can be used to calculate the group value.
    :return:
    """
    mahaDict = copy.deepcopy(save.dict_mahaDistance)
    for direction in mahaDict.keys():
        # get the array, and then flatten it into one dimension, then sort it
        array = mahaDict[direction]
        array = np.array(array)
        flattenArray = array.flatten()
        sortArray = sorted(flattenArray)

        # get the min and max number from the sort
        min = 0.0
        for value in sortArray:
            if value > 0.0:
                min = value
                break
        max = sortArray[-1]
        save.dict_mahaBorderValue.update({direction: [min, max]})
    return
