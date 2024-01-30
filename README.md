# TS-93
Hecho en C# con Unity 2020.3.14f1

[Jugar ahora](https://bojacket.itch.io/ts-93)

### Contexto
TS-03 es un plataformero 2D en el que se controla a un robot cuyo objetivo es destruir todos los servidores de cada nivel. Hay 3 niveles en total.

![image](https://user-images.githubusercontent.com/28521533/229397999-c433322f-94e4-4975-ab68-6165229899ba.png)
![image](https://user-images.githubusercontent.com/28521533/229398008-e6aec41a-dc07-40e2-b4d5-35ee4911cbac.png)
### Objetivo
El jugador empezará con 5 vidas para completar todo el juego (completar un nivel otorga 1 vida extra). Cada nivel tiene un tiempo límite para completarlo. Si el tiempo se acaba o el jugador pierde su última vida, el juego terminará y deberá volver al menú principal. Para destruir los servidores, el jugador debe conseguir dinamita. Entonces, cada nivel se podría dividir en dos partes: la primera consiste en conseguir la dinamita, y la segunda en usarla para destruir los servidores que se encuentren el nivel. Los niveles tendrán diversos obstáculos que el jugador deberá evitar, y varios no podrán ser superados sin ayuda de alguna de las herramientas que aparecerán en el nivel.
### Herramientas
- **Jetpack**: Permite saltar una infinita cantidad de veces sin necesidad de tocar el suelo.
- **Dinamita**: Ilimitada, se puede arrojar en cualquier momento. Al explotar, los servidores dentro de su radio de explosión serán destruidos, las cajas y las sierras saldrán volando, y el jugador también, además de quedar aturdido por unos segundos.
- **Linterna**: Da luz en un cono constantemente. Se puede manejar la dirección a alumbrar.
### Controles
- **A/D** para moverse
- **W** para saltar (mantener para saltar más alto)
- **F** para arrojar dinamita
- **Mover mouse** para apuntar con la linterna
### Mecánicas implementadas
- Movimiento del personaje mediante fuerzas
-	Tiempo coyote (al caer de una plataforma, el jugador tiene una fracción de segundo adicional para saltar)
-	Sistema de aturdimiento del personaje.
-	Arrojar bombas (mediante AddForce)
-	Dinamita capaz de empujar a los objetos y al jugador en un radio
-	Bolas de demolición (péndulo y Distance Joint)
-	Aturdir al jugador si es empujado por una explosión, toca una bola de demolición o toca una caja que está cayendo
-	Cintas transportadoras (Surface Effector)
-	Cajas y generadores de cajas usadas tanto para tapar el camino como para golpear y entorpecer al jugador
-	Luces (Light2D) y zonas oscuras
-	Linterna (Light2D)
-	Jetpack
-	Trampolines
-	Pinches que matan al jugador y lo hacen reaparecer
-	Sierras móviles (con RayCasting)
-	Sistema de reinicio del nivel al morir
-	Uso de un Material llamado Wall para quitarle la fricción a las paredes
