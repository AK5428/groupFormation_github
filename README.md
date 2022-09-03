# Read Me for this group formation system

## Introduction for the files

1. Read me: the introduction file;
1. Setup.exe: the executable file to install the interface system;
1. interface_C folder: the original code to build the interface system, written in C#;
1. groupFormation_python folder: the group formation system, written in python;

## Hardware and environment request

1. A computer with a Windows system.
2. Python environment and compiler.

## Software configuration

1. Double click on the setup.exe, install the software;
2. Find the groupFormation_python folder, open it with a python compiler (we suggest pycharm);
3. For any error that may occur while importing particular modules, please fix it by installing the modules;

## Get the key for similarity calculation

> We use tecent API to calculate the similarity. Please sign up to get a secret key, it's free.

1. Go to the website https://console.cloud.tencent.com/nlp to get the api;
2. Go to the console here https://console.cloud.tencent.com/cam/capi to get the Secret key and Secret id.

## File Preparation

The system supports several kinds of input data, which are explained in the paper. For all 4 type of data [value], [type], [network], [tag], we provide 7 kind of template. Users need to collect the data that are concerned useful for group formation and organize the data using the 7 kinds of templates.

### Template introduction

> The introduction for all the templates.

The templates and their categories are shown in the table below. 

|                | Type: value         | Type: type         | Type: network         | Type: tag                 | Others        |
| -------------- | ------------------- | ------------------ | --------------------- | ------------------------- | ------------- |
| template name1 | dimensionName_value | dimensionName_type | dimensionName_network | dimensionName_tag_similar | classNameList |
| template name2 |                     |                    |                       | dimensionName_tag_weight  |               |
| template name3 |                     |                    |                       | dimensionName_tag_mix     |               |

The exact reasons and explanations for the categories are shown in the paper. We only explain how to use the templates here.

Please follow the notes below:

1. All the data must be input using csv file;
2. Make sure the csv file is named "dimensionName_dimensionType.csv";
3. We only support 6 kinds of dimensionName, please check all the files fit the template;

### Preparation steps

1. Put all the csv files into a folder;
2. Open the python project, go to the save.py:
   1. Copy the absolute path of the folder to ORIGINAL_FOLDER;
   2. Change the dimension name in "allDimension" list, and make them fit your input file;
   3. Change the Chinese dimension name in "dimensionName_chinese" list in the same order. You can translate it into other languages, too, if you want;
   4. Change the groupNum (the number of the group) and minPNum (minimum number of the members in one group) to fit your request;
   5. Change the SecretId and SecretKey to the ones you just applied;
3. Go to preProcessMain.py, run it, and wait until it's finished;
4. Then the preparation is done.

## Use it

Using all the data you have input and processed in the preparation, we can now start the group formation process.

1. Go to the groupSelectMain.py, run it;
2. Double click to open the system you installed with the setup.exe;
3. Use it and follow the steps in the paper.

## Additional note

> Our system uses a genetic algorithm to calculate the best group formation. You can change some parameters to adjust it. All the parameters are in the save.py.

The adjusting you can make:

1. loopTime: you can make it larger to create a better result, but a larger number means a longer time to calculate. We suggest setting it between 500-3000.
2. gnerationNum: quite similar to loopTime, you can make the result better by making this number larger, but it requires more time. We suggest setting it between 10-30.

If you are familiar with genetic algorithms, then you can also change the following parameters:

1. chance4Individual: the chance one individual may mutate;
2. chance4Gene: the chance one gene may mutate;
