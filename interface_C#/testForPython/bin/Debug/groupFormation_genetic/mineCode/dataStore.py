"""
Used to store and read the data for the visual system.
"""
import numpy as np
from mineCode import save

# the path of the final results
folderName = save.ORIGINAL_FOLDER.split('_')[-1]
finalResultPath = save.BEST_QUEUE_FOLDER + '/' + folderName + '.npy'

# the path of the current progress
folderName = save.ORIGINAL_FOLDER.split('_')[-1]
currentProgressPath = save.CURRENT_PROGRESS_FOLDER + '/' + folderName + '.txt'

def currentProgressWrite():
    np.save(currentProgressPath, save.currentProcess, allow_pickle=True)
    with open(currentProgressPath, "w") as file:
        file.write(str(save.currentProcess))
    return

def currentProgressRead():
    return np.load(currentProgressPath, save.currentProcess, allow_pickle=True)

def currentBestQueueRead():
    return list(np.load(finalResultPath, save.list_finalResult, allow_pickle=True))[-1]


if __name__ == '__main__':
    with open(currentProgressPath, "r") as file:
        print(file.read())