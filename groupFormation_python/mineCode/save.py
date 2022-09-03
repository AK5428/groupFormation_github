"""
The py used to save all the static data.
"""
import collections

"""
the path for all files.
"""
# main path
#MAIN_FILE_PATH = "/Users/cosy/Projects/PycharmProjects/groupFormation_genetic_FrontEnd//"
MAIN_FILE_PATH ="C:/Users/DELL/source/repos/testForPython/testForPython/bin/Debug/groupFormation_genetic//"
# ORIGINAL_FOLDER = MAIN_FILE_PATH + 'mindData/originalInput/class_single'
ORIGINAL_FOLDER = ""
# ORIGINAL_FOLDER = MAIN_FILE_PATH + 'mindData/originalInput/class_former'
# ORIGINAL_FOLDER = '/Users/cosy/Desktop/class_former'
# similarities save, the folder path. The file name should be determined by the original path
SIMILARITY_FOLDER = MAIN_FILE_PATH + 'mindData/similarities'
# pre processed done, save the result into npy
PRE_PROCESSED_FOLDER = MAIN_FILE_PATH + 'mindData/preProcessed'
# the name list for the current class
NAME_LIST = ORIGINAL_FOLDER + '/classNameList.csv'
# the folderPath used to save all the best queues.
BEST_QUEUE_FOLDER = MAIN_FILE_PATH + 'mindData/bestQueues'
# the folderPath used to save the current progress
CURRENT_PROGRESS_FOLDER = MAIN_FILE_PATH + 'mindData/currentProgress'

# to see how many _ in folderPath
underLineNum = 2

"""
student class
"""
# store all the students object
list_student = []
list_colony = []

"""
Similarities calculate, in the similarCountPackage
"""
# store the false list and come back for it later
list_similarFalse = []
# if the similarities already been calculated
isFromFile = True
# the gap for the ones with no weight
gap_noWeight = 1

"""
Queue evaluation. For the evaluate package.
"""
# used to save all the queue as object
list_queueObject = []
# the fix value for the [type] kind of data
fixValue_type = 1

# the settings for isEquilibrium
isEquilibrium = True
# the value used to balance the variance and average
varianceDivisor = 0.01

"""
process for the network
"""
# the distance for different intimate
distanceClose = 10.0
distanceFar = 50.0

"""
homo/heter select and maha distance calculate
"""
allDimension = ['age', 'field', 'intimate', 'major', 'mbti']
# how dimension name trans into chinese
dimensionName_chinese = ['年龄', '擅长知识点', '亲密度', '专业', 'MBTI']
# used to translate the English into Chinese
dict_English2Chinese = {'age': '年龄', 'field': '擅长知识点', 'intimate': '亲密度', 'major': '专业', 'mbti': 'MBTI'}

heterDimension = ['field', 'major', 'age']
homoDimension = ['intimate', 'mbti']
dict_mahaDistance = {}
dict_mahaBorderValue = {}

"""
Group select
"""
# the queues I want to find for once
loopTime = 100
firstGenerationPopulation = loopTime
# groupNum
groupNum = 10
minPNum = 4

"""
Genetic algorithm
"""
""" the list for save """
# the parent generation, queue objects.
list_queueParent = []
# the son generation, queue objects.
list_queueSon = []

""" the parameters defined """
generationNum = 5
chosenIndividualNum = int(firstGenerationPopulation * 0.2)
crossIndividualNum = firstGenerationPopulation - chosenIndividualNum

"""the progress bar for the calculating."""
# the percent num
currentProcess = 0
# means 1 generation cost tenfold the time to generate one individual.
generationCount = firstGenerationPopulation / 25
# total amount
totalAmount = generationCount * generationNum + firstGenerationPopulation

"""probabilities for variance"""
chance4Individual = 0.25
chance4Gene = 0.01
# record the sum of the individual and gene that changes
currentGeneration = -1
individualChanged = 0
geneChanged = 0

"""result for one choice"""
currentBestQueue = None

"""result for all the choice, the best queue list"""
list_finalResult = []

"""
The tecent api info
"""
SecretId = ''
SecretKey = ''