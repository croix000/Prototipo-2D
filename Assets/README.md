# Prototipo juego 2D


## Descripción del juego

Bull et hell (Toro Y infierno) es un juego, "Bullet Hell" (infierno de balas) por turnos basado en movimientos por casillas. 

Los juegos "Bullet Hell" se caracterizan en juegos 2D Top down donde hay una enorme cantidad de balas y el jugador tiene que esquibarlas y disparar a sus enemigos.

En esta mezcla de géneros, al ser un juego por turnos, cada vez que el jugador realice una acción, pasará el turno. Cada enemigo ataca cada ciertos turnos, y las balas se mueven cada turno. Lo cual hace que el jugador tenga que pensar donde colocarse antes de disparar o moverse.


## Objetivo

El jugador tiene que recoger 3 cristales escondidos en el nivel para abrir el portal para poder completar el nivel.

## Condición de Fracaso

Si el jugador es golpeado por 2 balas muere.

## Controles

ASWD -Mover el jugador
Click derecho -Apuntar
Click Izquierdo -Disparar
R -Cargar otro nivel

# Especificaciones Tecnicas

-Todas las balas de los enemigos y del jugador pertenecen a un Object Pool.

-La interaccion del jugador con el gamemanager se hace a traves de eventos.

-Camara Cinemachine que tiene como objetivo al jugador.

-Generacion Procedural de niveles: primero se colocan los pisos, luego las paredes usando tilemaps, la sálida, enemigos y por último los cristales.

-Singleton en el manejo de sonidos, musica y SFX van al Soundmanager y dependiendo de los ajustes reproduce o no los sonidos.

-2 escenas el menu y el juego

-UI que muestra minimapa usando una segunda camara con rendertexture, vida y cristales del jugador.

-Manejador de turnos que cada vez que captura el evento de una accion del jugador genera un evento para el turno de los enemigos y proyectiles.

