# NullDC-NAOMI Netplay Launcher Distribution 0.6.3.1
Configuracion optimizada por blueminder *(Junio 21, 2020)*

Documentación traducida por BRBS



**Muchas Gracias**

- supersonicstep del Discord de FGC Arcadia
- MarioBrotha del Discord de FGC Arcadia(nullDCMultiLauncher, nullDCControlSetup)
- RossenX del Discord de FGC Arcadia por las optimizaciones al juego en linea
- pof & shine de [Fightcade](https://web.fightcade.com/) por la integracion y pruebas en general
- BRBS del Discord de FGC Arcadia Discord por ayudar en las preubas de cada beta y ayuda en general
- DQRF del Discord de GGXXACR(guilty gear)
- RaptorIIRC
- Teyah
- Jakstation
- reaVer

## Requisitos previos a la instalacion

Asegurate de tener instalado lo sgiguiene:

1. [DirectX](https://www.microsoft.com/en-us/download/details.aspx?id=35)
2. [Microsoft Visual C++ 2010 Redistributable Package (x86)](https://www.microsoft.com/en-us/download/details.aspx?id=5555)
3. [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net48)

Asegurate de tener los siguientes archivos antes de empezar

1. La BIOS de NAOMI (`naomi_bios.bin`)
2.Los ROMs  de los juegos NAOMI en formato `.zip` y que estos contengan los archivos `.lst` & `.bin`

## Instalando los ROMs

NullDC Netplay Launcher no provee ningun tipo de ROMs. Tu deberas encontrar y descargar la NAOMI BIOS, y cualquiera de los ROMs NAOMI que desees jugar por tu cuenta. Cualquier evento o comunidad en la que participes debera tener una lista de los ROMs aprobados para asegurar la compartibilidad entre oponentes, asi que puedes preguntar.

Una vez que tengas los archivos necesarios:

1. Abre `nullDCNetplayLauncher.exe`
2. Abre el menu *File Drop* presionando el boton que esta a la derecha del icono con forma de control(tiene forma de una flechita hacia arriba).
3. Localiza el `naomi_bios.bin` y los ROMs(en formato.zip) y arrastralos sobre el  cuadro. Cada ROM en formato .zip contener dentro el archivo `.bin`/`.dat` asi como el correspondiente archivo`.lst`.

![Demostración de File Drop](file_drop_demo.gif)

Si lo prefieres puedes hacerlo manualmente, solo coloca el archivo (`naomi_bios.bin`) en `nulldc-1-0-4-en-win\data` y coloca tus ROMs descomprimidos en `nulldc-1-0-4-en-win\roms` cada juego en su folder por separado(ejemplo: `nulldc-1-0-4-en-win\roms\mvc2`, el nombre del folder en el que esten los archivos del ROM sera usado para dicho juego en el launcher.

Archivos CDI y GDI ROMs (Dreamcast) no son soportados por el momento.

## Offline Setup (Juego Local)

1. Abre `nullDCNetplayLauncher.exe`

2. Presiona el icono del control en la esquina izquierda. Asegurate de que solo un control este conectado(fightstick, gamepad o el mando que vayas a utilizar) y sigue las instrucciones.

3. Selecciona cualkier ROM (juego) del menu y presiona el boton *Play Offline*

Esto ejecutara el ROM que seleccionaste en el emulador NullDC, dandote la oportunidad de probar tus controles. Si el juego te asigna el jugador 1 y tus controles funcionan bien ya estas listo para el juego en linea!

# Netplay Setup (Juego Online)

**(OPCIONAL, PERO RECOMENDADO)** [Radmin VPN](https://www.radmin-vpn.com/)

El metodo de juego en linea funciona para cualquier tipo de LAN local o virtual como Radmin VPN, ZeroTier o Hamachi. Radmin VPN es la VLAN usada por FGC Arcadia. Asegurate de tenerla instalada y activa antes de buscar partidas en el canal de Discord, pues tendras que crear o unisrte a una red con las personas con las que desees jugar.

Las sugientes instrucciones son asumiendo que ya tienes Radmin instalado, pero deberian funcionar igual para cualquier otro tipo de VLAN o LAN mientras tengas la IP de host correcta, puertos y tu firewallo no este bloqueando las conecciones o puertos relevantes.

1. Abre `nullDCNetplayLauncher.exe`


2. Elige el ROM que deseas jugar online


3.  Inicia sesion presionando el boton  *Host Game* o el boton *Join Game*

   - **Hosting a Game** (tu oponente se conecta a tu pc)

     1. Si tu eres el HOST asegurate de copiar tu IP de Radmin a la columna del launcher en donde dice  *Host IP*. Puedes salvar este cambio para que el launcher ya tenga tu ip de Radmin por defecto cada vez que seas host. 

     2. Copia la IP de tu contrincantede Radmin y pegala en la columna *Guest IP*. ahora, presiona el boton *Guess* para determinar el Delay apropiado basado en la distancia entre tu y tu oponente. Si la coneccion no fuera posible entre los 2 el cuadro de delay se pondra en color rojo.

     3. Presiona *Generate Host Code* y presiona el icono (Clipboard) a la derecha para copiar el enlace generado.

     4. Proporcionale ese codigo a tu openente(por discord o algun otro metodo de chat online), el pegara ese codigo en su respectivo emulador y todos los detalles de coneccion  se generan automaticamente para el. Otro metodo que podrias usar seria proporcionarle tu ip, puerto y delay, cualquiera de los 2 metodos funciona.

     5. Verifica que los 2 tengan el mismo Delay.

     6. Presiona el boton *Launch Game*.

     Espera un momento a que todo cargue y una vez que entres al juego si eres 'host' tu seras player 1 y tu oponente bplayer 2. Si es asi quiere decir que todo salio bien ahora a disfrutar tu partida.

![Demostración de Host Game](host_demo.gif)

   - **Joining a Game** (tu te conectas a la pc de tu oponente)
    
     1. Selecciona el ROM
       
     2. Presiona el boton *join Game* 
       
     3. Pega el codigo de host que tu oponente te envio y todos los detalles se llenaran automaticamente. Tambien puedes ingresar manualmente los datos de tu oponente(HOST IP, PORT, DELAY)  caulquiera de los 2 metodos funciona.
       
     
      Asegurate de que la IP de Radmin de tu oponente es la misma que la de la columna in the *Host IP* en el launcher. Asegurate que tu y tu oponente tengan seleccionado el mismo delay.

![Demostración de Join Game](join_demo.gif)

4. Presiona el boton *Launch Game*.

5. Juega!

Si experimentan algun tipo de alentamiento o cualquier tipo de desincronizacion, asegurense de experimentar con diferentes delays dentro del rango establecido. Toma algo de prueba y error, pero una vez que encuentras un valor correcto, es realmente genial y la jugabilidad es excelente. Solo asegurense de que los dos usen el mismo delay.

Una vez que encuentren los settings para una coneccion buena, puedes salvar esos parametros y asi tenerlos disponibles para la proxima vez que tengan una partida.

Solo en casa de que el boton *Guess* no sea confiable para ti, aqui estan unos parametros recomendados para el Delay basados en el ping promedio en milisegundos:

> < 25ms = 1 Delay Frame recomendado
> < 60ms = 2 Delay Frame recomendado
> < 100ms = 3 Delay Frame recomendado
> < 130ms = 4 Delay Frame recomendado
> < 155ms = 5 Delay Frame recomendado
> < 180ms = 6 Delay Frame recomendado
> mas de 180ms = no pierdas tu tiempo, es mejor buscar otro contrincante :(

## Command Line (lineas de comando)

El launcher ahora tiene opciones de lineas de comando, para que si asi lo deseas puedas integrarlo al emulador o sistema de lobby que gustes. Para ver todas las opciones, puedes ejecutar:

`nullDCNetplayLauncher.exe --help`

Los juegos deberan ser especificados en el archivo `games.json`, mostrandolos detalles del ROM como la descripcion y hubicacion. Loa juegos enlistados en este archivo puedes ser llamados usando `--gameid` :

`nullDCNetplayLauncher.exe --gameid cvs2 --offline 1`

De otra forma,puedes ejecutar los juegos usando la hubicacion:

`nullDCNetplayLauncher.exe --lst-path <path to rom lst> --offline 1`

Para hostear un juego:

`nullDCNetplayLauncher.exe --gameid cvs2 --hosting 1 --ip <ip address> --port <port> --delay <delay>`

para unirse a un juego:

`nullDCNetplayLauncher.exe --gameid cvs2 --hosting 0 --ip <ip address> --port <port> --delay <delay>`

Si quieres que el launcher adivine el dalay automaticamente puedes usar `--guess-ip`:

`nullDCNetplayLauncher.exe --gameid cvs2 --hosting 0 --ip <ip address> --port <port> --guess-ip <remote ip>`



## Solución de Problemas

> Mi control no es detectado por completo / tengo problemas con algunos inputs en la configuracion del control
>

Si tienes problemas para configurar tu control, intenta marcando la casilla *Force Keyboard Mapper*  en el menu del configuracion del control. Esto activara el mapeador de teclado interno del launcher que provee soporte extendido para controles no soportados en el plugin qkoJAMMA que se usa como default.

Si usas un stick/pad que cuente con la opcion *modo PS3* intenta usar este modo y no el *Keyboard Mapper*. Los controles de PS3 han desmostrado tener la mejor compatibilidad con el plug in qkoJAMMA.

------



> Cuando trato de unirme a un juego o hostear un juego, ninguno de los 2 logra conectarse al otro. Que debo hacer?

El primer paso para solucionar problemas de coneccion seria deshabilitar tu firewall para ver si de esa forma la coneccion funciona. Puedes hacer esto en el Panel de Control de "Windows Defender Firewall". Si pueden conectarse cuando el firewall esta deshabilitado, verifica en las opciones que `nullDC_Win32_Release-NoTrace.exe`  tiene permisos para acceder a redes privadas. Si eso no funciona habilita redes publicas desde ahi.

A la hora de intentar solucionar algun problema de coneccion, puedes pasar por el Discor de FGC Arcadia y preguntar en la seccion de ayuda,  pon atencion a la ventana de comandos para cualquier detalle. Esto le sera de ayuda a cualquier persona que te ayude .

------



> Accidentalmente active el Keyboard Mapper para mi control en el menu de configuracion. como lo desactivo?
>

  Para deshabilitar el Keyboard Mapper ve a opciones y desmarca la casilla *Enable Keyboard Mapper*. Esto revertira NulDC a los driver de control por defecto.

------



> Cuando juego ROMs Atomiswave , todos los botones actuan como el boton de creditos(coin) o start
>

 Verifica si el boton *test* presenta algun conflicto con el boton *start*. Es recomendable evitar(skip) el boton *test* a la hora de configurar el control, ya que no es necesario para jugar. Es recomendable asegurarse de que ninguno de los botones se repitan en la configuracion de tu control. Puedes verificar esto hechando un vistazo al archivo `.qjc` que correesponde a tu control en el directorio `nulldc-1-0-4-en-win\qkoJAMMA`. Puedes acceder a este directorio atravez de la pestaña de opciones avanzadas.

  Si no encuentras la manera de solucionar el problema con el control considera usar el Keyboard Mapper. Esto a sido la solucion major solucion para muchas personas con este problema en los juegos Atomiswave.

Si tienes mas preguntas tomate la libertad de pasar por el canal de discord oficial [FGC Arcadia](https://discord.com/invite/KczAkRr).