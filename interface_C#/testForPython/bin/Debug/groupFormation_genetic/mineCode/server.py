# !/usr/bin/python
# -*- coding: UTF-8 -*-
# python服务器如果需要访问静态的文件，都需要放到static这个指定的文件夹。
import csv

from flask import Flask, request
import save
import dataStore
import mineCode.genetic_groupSelect.groupSelectMain
import mineCode.genetic_groupSelect.geneticAlgorithm.queueClass
import mineCode.genetic_groupSelect.resultProcess
import numpy as np
# the path of the npy
folderName = save.ORIGINAL_FOLDER.split('_')[-1]
savePath = save.BEST_QUEUE_FOLDER + '/' + folderName + '.npy'


app = Flask(__name__)
app.logger.info('Finished Start Flask!')

# 获取班级信息
@app.route('/getClass/', methods=['POST'])
def SendClassInfo(name=None):
    if request.method == 'POST':
        return str(save.NAME_LIST)

# 获取维度信息
@app.route('/getDimension/', methods=['POST'])
def SendDimensionInfo(name=None):
    if request.method == 'POST':
        return str(save.allDimension+save.dimensionName_chinese)

# 删除一次结果记录
@app.route('/deleteAResult/', methods=['POST'])
def deleteAResult(name=None):
    if request.method == 'POST':
        receiveData = request.data.decode('utf-8')  # 为了兼容中文输入
        num = int(receiveData)
        mineCode.genetic_groupSelect.resultProcess.queueDelete(num)
        return "OK"
# 获取计算进度
@app.route('/getProcess/', methods=['POST'])
def GetProcess(name=None):
    if request.method == 'POST':
        return str(dataStore.currentProgressPath)
# 清除已有所有结果
@app.route('/clearAllRes/', methods=['POST'])
def ClaerAllItems(name=None):
    if request.method == 'POST':
        mineCode.genetic_groupSelect.resultProcess.queueClear()

# 获取单次计算结果
@app.route('/GetCalRes/', methods=['POST'])
def GetCalRes(name=None):
    if request.method == 'POST':
        receiveData = request.data.decode('utf-8')  # 为了兼容中文输入
        para = int(receiveData)
        print(singleBest2String(para))
        return singleBest2String(para)
# 计算进度初始化
@app.route('/ResetProcess/', methods=['POST'])
def ResetProcess(name=None):
    if request.method == 'POST':
        # the path of the current progress
        folderName = save.ORIGINAL_FOLDER.split('_')[-1]
        currentProgressPath = save.CURRENT_PROGRESS_FOLDER + '/' + folderName + '.txt'
        with open(currentProgressPath, "w") as file:
            file.write(str(0))
        return str("OK")

# 获取计算分组
@app.route('/getGroupData/', methods=['POST'])
def GetProupData(name=None):
    if request.method == 'POST':
        return resultList2String()
# 导出地址
@app.route('/SendSavePath/', methods=['POST'])
def SendSavePath(name=None):
    if request.method == 'POST':
        receiveData = request.data.decode('utf-8')  # 为了兼容中文输入
        para = str(receiveData)

        array = str(para).split('#')
        num = int(array[0])
        path = array[1]
        nameListExport(num,path)
        return resultList2String()

# 修改同质异质以及是否均衡
@app.route('/setHeterData/', methods=['POST'])
def GetHomoInfo(name=None):
    if request.method == 'POST':
        receiveData = request.data.decode('utf-8')  # 为了兼容中文输入
        para = str(receiveData)


        array = str(para).split('#')
        homos = str(array[1]).split('-')
        heters = str(array[2]).split('-')

        save.homoDimension = homos
        save.heterDimension = heters

        if(array[3] =="False"):
            save.isEquilibrium = False
        else:
            save.isEquilibrium = True
        mineCode.genetic_groupSelect.groupSelectMain.groupSelectStart(save.homoDimension,
        save.heterDimension, save.isEquilibrium)
        return str("Finished")

def nameListExport(num, filePath):
    """
    Used to corresponding to the name list export btn.
    :param num: which result should we export.
    :param filePath: where the user hopes the file is. In csv format.
    :return: None.
    """
    # get the queue from file
    bestQueue = np.array(np.load(savePath, save.list_finalResult, allow_pickle=True))[num]

    # initial the csv file to write
    csvFile = open(filePath, 'w')
    csvWriter = csv.writer(csvFile)
    # write in the head
    csvWriter.writerow(['组号', '学生ID', '学生姓名'])

    # loop to write the student
    # find the group
    for groupNum in range(len(bestQueue.list_groupObject)):
        group = bestQueue.list_groupObject[groupNum]
        # find the students
        firstStu = group.list_studentObjects[0]
        csvWriter.writerow([groupNum + 1, firstStu.stuId, firstStu.stuName])
        for stu in group.list_studentObjects[1:]:
            csvWriter.writerow([None, stu.stuId, stu.stuName])
    # print(bestQueue)
    return

def resultList2String():
    """
    The function used to return the string for the result list.
    Corresponding to the first page.
    :return: string
    """
    # the string to store all the final result, separate with #
    string4store = ''
    list_finalResult = np.array(np.load(savePath, allow_pickle=True))

    """loop to arrange the string for all the queues"""
    for queue in list_finalResult:
        # used to store the string for one single queue
        singleQueueString = ''

        # the string for homoList, except the last one

        if (len(queue.homoChoiceList) > 0):
            for i in range(len(queue.homoChoiceList)):
                dimension = queue.homoChoiceList[i]
                if dimension == '':
                    singleQueueString += '|'
                    continue
                singleQueueString += str(save.dict_English2Chinese[dimension])
                singleQueueString += ','
            # add the last one
            singleQueueString = singleQueueString[:-1] + '|'

        # print(singleQueueString)

        # the string for homoList, except the last one
        if (len(queue.heterChoiceList) > 0):
            for i in range(len(queue.heterChoiceList)):
                dimension = queue.heterChoiceList[i]
                if dimension == '':
                    singleQueueString += '|'
                    continue
                singleQueueString += str(save.dict_English2Chinese[dimension]) + ','
            # add the last one
            singleQueueString = singleQueueString[:-1] + '|'
        # print(singleQueueString)

        # homoDistance, heterDistance, variance
        singleQueueString += str(queue.homoDAverage) + ','
        singleQueueString += str(queue.heterDAverage) + ','
        singleQueueString += str(queue.queueVariance) + '|'
        # print(singleQueueString)

        # the name list of the students
        studentList = queue.bestGroup.list_studentObjects
        # print(studentList)
        for i in range(len(studentList) - 1):
            singleQueueString += str(studentList[i].stuId) + '&' + str(studentList[i].stuName) + ','
        singleQueueString += str(studentList[-1].stuId) + '&' + str(studentList[-1].stuName)

        string4store += singleQueueString + '#'

    return string4store[:-1]

        # print(queue)

def singleBest2String(num):
    """
    Used to return one single result into string.
    Corresponding to the final page.
    :param num: if the user click from the first page, input the num
    else, input -1 to get the current best queue.
    :return:
    """
    bestQueue = np.array(np.load(savePath, allow_pickle=True))[num]
    string4store = ''

    # the homo/heter distance and the virance for the result
    string4store += str(bestQueue.homoDAverage) + ',' + str(bestQueue.heterDAverage) + ',' + \
                    str(bestQueue.queueVariance) + '|'

    # get the string for each group
    for group in bestQueue.list_groupObject:
        singleString = str(group.homoDistance) + ',' + str(group.heterDistance) + ','
        for student in group.list_studentObjects:
            singleString += str(student.stuId) + '&' + str(student.stuName) + ','

        # add the group string to the whole string
        string4store += singleString[:-1] + '|'

    return string4store[:-2]

if __name__ == '__main__':
    app.run(host='127.0.0.1', port=8000, debug=False, threaded=True)
    # debug=True 时设置的多线程无效
    # 多线程和多进程功能只能开一个     1.processes=True      2.threaded=True