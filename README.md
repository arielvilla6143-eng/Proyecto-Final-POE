# Sistema de Préstamo de Libros - Biblioteca

## Actividad 2.4 Reestructuración de Vistas con Herencia Gráfica y Polimorfismo en Botones
Equipo 2

## integrantes
Joule Alexander Suñiga Maciel
Ariel Villa Olivares
Marco Antonio Nuñes Martines  

## Descripción
En la actividad 2.2 todas las entidades estaban en una sola ventana. Ahora cada entidad (Usuario, Administrador, Autor, Categoria, Editorial, Libro, Ejemplar, Prestamo, Sancion y Pago) tiene su propio formulario, que hereda de un formulario base. Un formulario principal con botones únicos controla cualquiera de ellos a través de una interfaz, sin saber cuál es.

## Interfaz IPanelCRUD
Es el contrato que cumplen todos los formularios de entidad. Tiene 4 métodos:
- EjecutarGuardar()
- EjecutarBuscar(string id, ErrorProvider alerta)
- EjecutarActualizar()
- EjecutarEliminar(StatusStrip barraEstado)

## Formulario base (FrmBase)
Es la plantilla común. Tiene un panel llamado pnlFormularioBase, con Modifiers en Protected para que los formularios hijos lo puedan heredar, y la caja de texto txtId, donde se captura el Id que todas las entidades heredan de EntidadBase.

## Formularios de las entidades
Cada uno hereda de FrmBase e implementa IPanelCRUD, por ejemplo FrmAutor : FrmBase, IPanelCRUD. Los controles propios de la entidad se acomodan dentro del panel heredado. Los 4 métodos usan el CRUD de los modelos de la 2.3:
- Guardar toma los datos de los controles, crea el objeto y lo guarda en la lista en memoria.
- Buscar consulta por el id y, si lo encuentra, regresa los datos a los controles. Si no existe, lo marca con el ErrorProvider.
- Actualizar reemplaza el registro con los datos de la pantalla y conserva lo que el formulario no edita, como la fecha de registro.
- Eliminar borra el registro y avisa en la barra de estado.

## Formulario principal (FrmPrincipal)
Es la ventana raíz. Tiene un panel central (pnlContenedorVistas) donde se muestra el formulario del módulo que se elige, un campo de búsqueda (txtIdBusqueda), un ErrorProvider, un StatusStrip abajo y los botones btnMasterGuardar, btnMasterBuscar, btnMasterActualizar y btnMasterEliminar.

Tiene una variable private IPanelCRUD vistaActiva. Cuando se elige un módulo, se crea su formulario, se le pone TopLevel = false y FormBorderStyle = None, se mete en el panel con Dock = Fill y se guarda en vistaActiva. Los botones no conocen las clases de los hijos, por ejemplo el de guardar solo llama a vistaActiva.EjecutarGuardar().

## Modelos
Las entidades vienen de la actividad 2.3: heredan de EntidadBase e implementan IAlmacenamientoCRUD. Como todavía no hay base de datos, cada una guarda sus registros en una lista estática en memoria.

## Manejo de errores
Los modelos validan sus datos y lanzan ArgumentException, y el CRUD lanza InvalidOperationException si el id ya existe o no existe. Los formularios capturan esos errores
