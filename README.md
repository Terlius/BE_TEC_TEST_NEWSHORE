##Proyecto ASP.NET CORE 6. 


Capas: API, BUSINESS Y DATAACCESS


Descripción
Prueba tecnica FLYR capa BackEnd que consume API NewShore https://recruiting-api.newshore.es/api/flights/1 - Rutas multiples

##Estructura del Proyecto
API: Esta capa se encarga de la interacción con el usuario, manejo de solicitudes HTTP y respuestas.
Business: Aquí se implementa toda la lógica de negocio y el servicio que crea las rutas de forma recursiva usando los modelos Journey, Flight y Transport
DataAccess: Esta capa se encarga de consumir la API, tiene los modelos FlightAPI y TransportAPI para mapear 

##Uso
Instalar Visual Studio 2022 al ingresar al programa darle en clonar repositorio, ingresar `https://github.com/Terlius/BE_TEC_TEST_NEWSHORE` y ejecutar la solución.
