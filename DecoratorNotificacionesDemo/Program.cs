using System;

namespace PatronDecoratorDemo
{
    // 1. Component (Interfaz común)
    public interface INotificador
    {
        void Enviar(string mensaje);
    }

    // 2. ConcreteComponent (Implementación base)
    public class NotificadorBase : INotificador
    {
        public void Enviar(string mensaje)
        {
            Console.WriteLine($"[Email] Enviando correo electrónico: {mensaje}");
        }
    }

    // 3. Decorator (Clase abstracta envolvente)
    public abstract class NotificadorDecorador : INotificador
    {
        protected INotificador _wrappee; // Referencia al objeto envuelto

        public NotificadorDecorador(INotificador wrappee)
        {
            _wrappee = wrappee;
        }

        public virtual void Enviar(string mensaje)
        {
            _wrappee?.Enviar(mensaje);
        }
    }

    // 4. ConcreteDecorator A (Añade capacidad de WhatsApp)
    public class WhatsAppDecorador : NotificadorDecorador
    {
        public WhatsAppDecorador(INotificador wrappee) : base(wrappee) { }

        public override void Enviar(string mensaje)
        {
            base.Enviar(mensaje); // Ejecuta el comportamiento previo
            EnviarPorWhatsApp(mensaje); // Añade el nuevo comportamiento
        }

        private void EnviarPorWhatsApp(string mensaje)
        {
            Console.WriteLine($"[WhatsApp] Enviando mensaje de texto al celular: {mensaje}");
        }
    }

    // 5. Programa principal de prueba
    class Program
    {
        static void Main(string[] args)
        {
            // Se crea el notificador base (solo correo)
            INotificador notificadorSimple = new NotificadorBase();

            Console.WriteLine("--- Notificación Base ---");
            notificadorSimple.Enviar("Alerta de sistema.");

            // Se decora el notificador añadiéndole dinámicamente WhatsApp
            INotificador notificadorConWhatsApp = new WhatsAppDecorador(notificadorSimple);

            Console.WriteLine("\n--- Notificación Decorada (Email + WhatsApp) ---");
            notificadorConWhatsApp.Enviar("¡Falla crítica en el servidor principal!");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}