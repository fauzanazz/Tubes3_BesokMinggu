import time
import tkinter as tk
from tkinter import filedialog
from PIL import Image, ImageTk
import numpy as np
import cv2
from normalization import normalization, orientation

def read_image(file_name):
    try:
        image = Image.open(file_name)
        return image
    except FileNotFoundError:
        return None

def image_segmentation(image, block_size, threshold):
    image_array = np.array(image)
    width, height, _ = image_array.shape

    num_blocks_horizontal = width // block_size
    num_blocks_vertical = height // block_size

    recreated_image = np.zeros(image_array.shape, dtype=np.uint8)
    
    # Iterate over each block
    for i in range(num_blocks_vertical):
        for j in range(num_blocks_horizontal):
            # Crop the block
            left = j * block_size
            right = left + block_size
            top = i * block_size
            bottom = top + block_size

            block = image_array[top:bottom, left:right, :]
            mean_color = np.sum(block, axis=(0, 1)) / (block_size * block_size)
            variance_color = np.mean((block - mean_color) ** 2, axis=(0, 1))

            block_type = 0 if np.all(variance_color < threshold) else 255
            
            recreated_image[top:bottom, left:right, :] = block_type
    
    return recreated_image

def core_marks_in_block(orientation_field, block_center, threshold):
    marks = 0
    for i in range(block_center[0] - 1, block_center[0] + 2):
        for j in range(block_center[1] - 1, block_center[1] + 2):
            if orientation_field[i, j] > threshold:
                marks += 1
    return marks

def find_core_point(orientation_field, threshold):
    max_marks = 0
    max_core_point = None
    for i in range(1, orientation_field.shape[0] - 1):
        for j in range(1, orientation_field.shape[1] - 1):
            if orientation_field[i, j] > threshold:
                marks = core_marks_in_block(orientation_field, (i, j), threshold)
                if marks > max_marks:
                    max_marks = marks
                    max_core_point = (i, j)
    return max_core_point

def draw_core_point(image, core_point):
    image_with_core_point = np.array(image)
    x, y = core_point
    # Draw red dot (circle) at the core point location
    cv2.circle(image_with_core_point, (y, x), radius=3, color=(255, 0, 0), thickness=-1)
    return image_with_core_point

def process_image(image):
    block_size = 5
    threshold = 100
    
    # Segment the image
    recreated_image = image_segmentation(image, block_size, threshold)
    
    # Convert to grayscale and normalize
    grayscale_image = Image.fromarray(recreated_image).convert("L")
    normalized_image = normalization(np.array(grayscale_image))

    # Calculate orientation field
    orientation_field, Vx_array, Vy_array = orientation(normalized_image, block_size)


    # Find core point
    core_threshold = 0.5  # You can adjust this threshold as needed
    core_point = find_core_point(normalized_image, core_threshold)

    # Draw core point on the image
    image_with_core_point = draw_core_point(normalized_image, core_point)

    return image_with_core_point

def display_image(image):
    Image.fromarray(image).show()

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

    # Display input image
    input_image.show("Input Image")
    time.sleep(3)

    # Process image
    output = process_image(input_image)

    # Display processed image with core point
    display_image(output)

if __name__ == "__main__":
    main()
