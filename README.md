# Prototipo juego 2D


![](gif.gif)


## Descripción del juego

Bull et hell (Toro e infierno) es un juego, "Bullet Hell" (infierno de balas) por turnos basado en movimientos por casillas. 

Los juegos "Bullet Hell" se caracterizan en juegos 2D Top down donde hay una enorme cantidad de balas y el jugador tiene que esquivarlas y disparar a sus enemigos.

En esta mezcla de géneros, al ser un juego por turnos, cada vez que el jugador realice una acción, pasará el turno. Cada enemigo ataca cada ciertos turnos, y las balas se mueven cada turno. Lo cual hace que el jugador tenga que pensar dónde colocarse antes de disparar o moverse.


## Objetivo

El jugador tiene que recoger 3 cristales escondidos en el nivel para abrir el portal para poder completar el nivel.

## Condición de Fracaso

Si el jugador es golpeado por 2 balas muere.

## Controles

ASWD -Mover el jugador
Click derecho -Apuntar
Click Izquierdo -Disparar
R -Cargar otro nivel

# Especificaciones Técnicas

-Todas las balas de los enemigos y del jugador pertenecen a un Object Pool.

-La interacción del jugador con el gamemanager se hace a través de eventos.

-Camara Cinemachine que tiene como objetivo al jugador.

-Generación Procedural de niveles: primero se colocan los pisos, luego las paredes usando tilemaps, la salida, enemigos y por último los cristales.

-Singleton en el manejo de sonidos, música y SFX van al Soundmanager y dependiendo de los ajustes reproduce o no los sonidos.

-2 escenas el menú y el juego

-UI que muestra minimapa usando una segunda cámara con rendertexture, vida y cristales del jugador.

-Manejador de turnos que cada vez que captura el evento de una acción del jugador genera un evento para el turno de los enemigos y proyectiles.
