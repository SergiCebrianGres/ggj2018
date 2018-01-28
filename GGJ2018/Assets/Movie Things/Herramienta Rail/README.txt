Setup Rail:

1. Abrir proyecto Unity.
2. Arrastrar los 3 scripts de la carpeta al proyecto (Rail, RailEditor, RailMover)
3. En "Hierarchy" eliminar el FPSController.
4. En "Hierarchy" Create -> Empty
5. En el GameObject creado; en "Inspector", resetear la posición del "Transform" y arrastrar el script Rail.cs.
6. Crear el rail moviendo la camara (click derecho + WASD) i haciendo click en el botón Add Node.
7. Cuando tengamos el rail hecho, en el inspector del objecto "Main Camera" arrastrar el script RailMover.cs
8. En el RailMover de la camara arrastrar al campo Rail el objeto al que le hemos puesto el rail.
9. Ya podemos ejecutar con el boton de "PLAY"

Opciones:
- Para reordenar los nodos del rail, moverlos en la jerarquia (son hijos del rail) y hacer click en Reorder Nodes en el script de Rail.
- Opciones de camara: 
	En RailMover, Linear va en linea recta entre los nodos, catmull suaviza las transiciones.
	Looping repite el camino,  Pingpong repite el camino en direccion opuesta.