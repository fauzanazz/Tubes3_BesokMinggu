import tkinter as tk
from tkinter import filedialog
from PIL import Image, ImageTk

def read_image(file_name):
    try:
        image = Image.open(file_name)
        return image
    except FileNotFoundError:
        return None

def process_image(image):
    # Placeholder function for image processing
    # You can extend this function to perform actual image processing tasks
    return image

def display_image(image):
    root = tk.Tk()
    root.title("Image Display")

    # Convert PIL Image to Tkinter PhotoImage
    photo_image = ImageTk.PhotoImage(image)

    # Display image on a label
    label = tk.Label(root, image=photo_image)
    label.pack()

    root.mainloop()

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
    display_image(input_image)

    # Process image (placeholder)
    output_image = process_image(input_image)

    # Display processed image
    display_image(output_image)

if __name__ == "__main__":
    main()
