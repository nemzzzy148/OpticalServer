import json
import os

#made by ai

# Get the folder where this script is located
script_dir = os.path.dirname(os.path.abspath(__file__))

# --- INPUT FILE ---
input_file = os.path.join(script_dir, "mesh.txt")

# --- OUTPUT FILE ---
output_file = os.path.join(script_dir, "level.json")

vertices = []
faces = []

with open(input_file, "r") as f:
    for line in f:
        line = line.strip()
        if not line:
            continue
        parts = line.split()
        if len(parts) == 9:
            # Vertex line: x y z r g b a s t
            vertex = [float(parts[0]), float(parts[1]), float(parts[2])] + \
                     [int(parts[3]), int(parts[4]), int(parts[5]), int(parts[6])] + \
                     [float(parts[7]), float(parts[8])]
            vertices.append(vertex)
        elif len(parts) >= 2 and parts[0].isdigit():
            # Face line: n v0 v1 v2 ... (skip n if first number)
            face_indices = list(map(int, parts[1:]))
            faces.append(face_indices)

# --- CREATE MESH OBJECTS ---
mesh_objects = []

for face in faces:
    verts = []
    colors = []
    for idx in face:
        v = vertices[idx]
        # Flatten vertices to 2D X,Y for game
        verts.extend([v[0], v[1]])
        colors.append([v[3]/255, v[4]/255, v[5]/255, v[6]/255])
    
    # Average color for the face
    r = sum(c[0] for c in colors) / len(colors)
    g = sum(c[1] for c in colors) / len(colors)
    b = sum(c[2] for c in colors) / len(colors)
    a = sum(c[3] for c in colors) / len(colors)
    
    # Triangles: quad to 2 triangles if face has 4 vertices
    if len(face) == 4:
        tris = [0,2,1,0,3,2]
    else:
        # Triangle
        tris = list(range(len(face)))
    
    mesh_objects.append({
        "mirror": False,
        "collision": True,
        "drawable": False,
        "r": r,
        "g": g,
        "b": b,
        "a": a,
        "vertices": verts,
        "triangles": tris
    })

# --- BUILD LEVEL JSON ---
level_data = {
    "name": "My_Level",
    "size": 1.0,
    "meshObjects": mesh_objects
}

# --- SAVE JSON ---
with open(output_file, "w") as f:
    json.dump(level_data, f, indent=2)

print(f"Exported {len(mesh_objects)} mesh objects to {output_file}")