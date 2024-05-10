import time
import numpy as np
from PIL import Image
from tkinter import filedialog

# global variable
blockSize = 15
blockCount = 36
M0 = 100
VAR0 = 100

def normalization(image):
    row, col = image.shape
    print("row,col = ", row, col)

    # Mean and variance calculation
    M = (1 / (row ** 2)) * np.sum(image)
    print("M = ", M)
    Mnp = np.mean(image)
    print("Mnp = ", Mnp)

    VAR = (1 / (row ** 2)) * np.sum((image - M) ** 2)
    print("VAR = ", VAR)
    VARnp = np.var(image)
    print("VARnp = ", VARnp)

    G = np.zeros(image.shape)
    for i in range(row):
        for j in range(col):

            if image[i, j] > M:
                G[i, j] = M0 + np.sqrt((VAR0 * (image[i, j] - M) ** 2))
            else:
                G[i, j] = M0 - np.sqrt((VAR0 * (image[i, j] - M) ** 2))
    return G

def orientation(image):
    # Calculate gradients gx, gy
    gx, gy = np.gradient(image)

    # Calculate gradient magnitude and direction (in degrees)
    mag = np.hypot(gx, gy)
    angle = np.arctan2(gy, gx) * 180 / np.pi

    # Create orientation field
    orientation_field = np.zeros(image.shape)
    for i in range(image.shape[0]):
        for j in range(image.shape[1]):
            if mag[i][j] > 50:
                orientation_field[i][j] = angle[i][j]
    return orientation_field

def smoothing(image, orientation_field):

    # Create smoothed image
    smoothed_image = np.zeros(image.shape)
    for i in range(image.shape[0]):
        for j in range(image.shape[1]):
            if image[i][j] > 50:
                smoothed_image[i][j] = image[i][j]
    return smoothed_image


def read_image(file_name):
    try:
        image = Image.open(file_name)
        return image
    except FileNotFoundError:
        return None

def main():

    # Ask user to select input file
    input_file = filedialog.askopenfilename(title="Select Input Image File", filetypes=[("Image files", "*.jpg;*.jpeg;*.png;*.gif;*.bmp")])

    if not input_file:
        print("No input file selected. Exiting...")
        return

    # Read input image
    input_image = read_image(input_file)
    if not input_image:
        print("Image not found. Exiting...")
        return
    
    # display input_image
    input_image.show(title="Input Image")
    
    # Change image to grayscale and convert to numpy array
    input_image = input_image.convert("L")   
    input_image = np.array(input_image)
    time.sleep(2) 

    print(input_image)

    # Normalize image
    normalize_image = normalization(input_image)
    print("\nNormalize image: ")
    print(normalize_image)

    # Orientation image
    orientation_estimation = orientation(normalize_image)
    print("\nOrientation estimation: ")
    print(orientation_estimation)

    # Smoothing and Finetuning
    smoothing_image = smoothing(normalize_image, orientation_estimation)
    print("\nSmoothing image: ")
    print(smoothing_image)

    
    
    # display normalize_image
    normalize_image = Image.fromarray(normalize_image)
    normalize_image.show(title="Normalize Image")

    time.sleep(2)

    # display smoothing_image
    smoothing_image = Image.fromarray(smoothing_image)
    smoothing_image.show(title="Smoothing Image")





if __name__ == "__main__":
    main()
