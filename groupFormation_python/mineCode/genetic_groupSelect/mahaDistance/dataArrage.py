"""
Used to combine the homo/heter separately.
1. judge the type of the dimensions, and process them with the corresponding way.
"""
from mineCode import save

def dataArrangeStart():
    """
    To start the arrangement of the datas, saved into homo/heter vectors.
    For the [type] kind, store the dimension name in list.
    :return:
    """
    # for each student, process the dimensions, combined the homo and heter
    # for the [type] kind of data, store them in the corresponding place
    for student in save.list_student:
        # for homo
        for dimension in save.homoDimension:
            if dimension in student.dict_type.keys():
                student.list_homoType.append(dimension)
            else:
                dimensionCombine('homo', dimension, student)

        # for heter
        for dimension in save.heterDimension:
            if dimension in student.dict_type.keys():
                student.list_heterType.append(dimension)
            else:
                dimensionCombine('heter', dimension, student)


def dimensionCombine(listChoice, dimension, student):
    """
    To find the data of the exact dimension, then save in the corresponding vector.
    :param listChoice: homo/heter
    :param dimension: current dimensino
    :param student: this exact student object
    :return: the updated objects
    """
    # is homo or heter
    if listChoice == 'homo':
        thisList = student.homoVector
    else:
        thisList = student.heterVector

    # judge the type of dimension
    if dimension in student.dict_value:
        thisList.append(float(student.dict_value[dimension]))
    elif dimension in student.dict_tag:
        thisList.extend(student.dict_tag[dimension])
    elif dimension in student.dict_network:
        thisList.extend(student.dict_network[dimension])
