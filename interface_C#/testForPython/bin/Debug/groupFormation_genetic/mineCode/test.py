import numpy as np

def test():
    x = np.array([[1, 2], [3, 4], [5, 6]])
    y = x.flatten()
    z = y.reshape((2, -1), order='F').T

    print('original: ', x)
    print('flatten: ', y)
    print('reshape： ', z)


if __name__ == '__main__':
    test()